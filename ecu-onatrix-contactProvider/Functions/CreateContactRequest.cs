using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ecu_onatrix_contactProvider.Functions;

public class CreateContactRequest(ILogger<CreateContactRequest> logger, IContactRequestService contactRequestService)
{
    private readonly ILogger<CreateContactRequest> _logger = logger;
    private readonly IContactRequestService _contactRequestService = contactRequestService;

    [Function("CreateContactRequest")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        try
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var cReq = JsonConvert.DeserializeObject<ContactRequest>(body);

            if (cReq == null)
                return new BadRequestObjectResult(new { Error = "Please provide a valid request" });

            using var ctsTimeOut = new CancellationTokenSource(TimeSpan.FromSeconds(120 * 1000));
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ctsTimeOut.Token, req.HttpContext.RequestAborted);

            var res = await _contactRequestService.CreateContactAsync(cReq, cts.Token);

            switch (res.Status)
            {
                case "200":
                    return new OkObjectResult(res.ContactRequest);
                case "400":
                    return new BadRequestObjectResult(new { Error = $"Function CreateSubscriber failed :: {res.Error}" });
                case "500":
                    return new ObjectResult(new { Error = $"Function CreateSubscriber failed :: {res.Error}" }) { StatusCode = 500 };
                default:
                    return new ObjectResult(new { Error = $"Function CreateSubscriber failed :: Unknown Error" }) { StatusCode = 500 };
            }
        }
        catch (Exception ex)
        {
            return new ObjectResult(new { Error = $"Function CreateSubscriber failed :: {ex.Message}" }) { StatusCode = 500 };
        }
    }
}
