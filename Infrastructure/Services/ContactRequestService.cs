
using ecu_onatrix_contactProvider.Data.Contexts;
using ecu_onatrix_contactProvider.Data.Entities;
using EmailSender;
using EmailSender.Models;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public interface IContactRequestService
{
    public Task<ContactResult> CreateContactAsync(ContactRequest cReq, CancellationToken cts);

    //public Task<ContactResult> GetContactAsync(ContactRequest sReq, CancellationToken cts);

    //public Task<ContactResult> GetAllContactAsync(CancellationToken cts);

    //public Task<ContactResult> UpdateContactAsync(ContactRequest sReq, CancellationToken cts);

    //public Task<ContactResult> DeleteContactAsync(ContactRequest sReq, CancellationToken cts);
}

public class ContactRequestService(IDbContextFactory<DataContext> contextFactory, IEmailSender emailSender) : IContactRequestService
{
    private readonly IDbContextFactory<DataContext> _contextFactory = contextFactory;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task<ContactResult> CreateContactAsync(ContactRequest cReq, CancellationToken cts)
    {
        if (cReq != null && cReq.Email != null)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();

                ContactEntity contactEntity = new ContactEntity
                {
                    Name = cReq.Name,
                    Email = cReq.Email,
                    Phone = cReq.Phone,
                    Category = cReq.Category,
                    Question = cReq.Question
                };

                await context.ContactRequests.AddAsync(contactEntity, cts);
                await context.SaveChangesAsync(cts);

                var entity = await context.ContactRequests.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Email == cReq.Email, cts);

                if (entity != null)
                { 
                    InputModel inputModel = ContactFactory.CreateEmailInput(entity);
                    
                    if(_emailSender.SendEmailAsync(inputModel).Result)
                        return new ContactResult { Status = "200", ContactRequest = ContactFactory.Create(entity) };
                    else
                        return new ContactResult { Status = "500", Error = "New contact request email not sent to requester" };
                }
                else
                    return new ContactResult { Status = "500", Error = "New contact request not created" };

            }
            catch (Exception ex) { return new ContactResult { Status = "500", Error = ex.Message }; }
        }

        return new ContactResult { Status = "400", Error = "Bad request" };
    }
}