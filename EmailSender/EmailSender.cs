using Azure.Messaging.ServiceBus;
using EmailSender.Factories;
using EmailSender.Models;
using Newtonsoft.Json;

namespace EmailSender;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(InputModel model);
}

public class EmailSender : IEmailSender
{
    private readonly string connectionString = "Endpoint=sb://sb-jb-onatrix.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=6p5CMXAEUzqN4vrTXyDrSSGdgP8p3PQKa+ASbDCxX6Q=";
    private readonly string queueName = "email_request";

    public async Task<bool> SendEmailAsync(InputModel inputModel)
    {
        try
        {
            if (inputModel != null)
            {
                await using var client = new ServiceBusClient(connectionString);
                ServiceBusSender sender = client.CreateSender(queueName);

                EmailModel emailModel = EmailFactory.Create(inputModel);

                ServiceBusMessage message = new ServiceBusMessage(JsonConvert.SerializeObject(emailModel));

                await sender.SendMessageAsync(message);
                    
                return true;
            }
        }
        catch {}

        return false;
    }
}
