
using ecu_onatrix_contactProvider.Data.Contexts;
using ecu_onatrix_contactProvider.Data.Entities;
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

public class ContactRequestService(IDbContextFactory<DataContext> contextFactory) : IContactRequestService
{
    private readonly IDbContextFactory<DataContext> _contextFactory = contextFactory;

    public async Task<ContactResult> CreateContactAsync(ContactRequest cReq, CancellationToken cts)
    {
        if (cReq != null && cReq.Email != null)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();

                if (!await context.ContactRequests.AnyAsync(x => x.Email == cReq.Email, cts))
                {
                    ContactEntity contactEntity = new ContactEntity
                    {
                        Name = cReq.Name,
                        Email = cReq.Email,
                        Phone = cReq.Phone,
                        Category = cReq.Category
                    };

                    await context.ContactRequests.AddAsync(contactEntity, cts);
                    await context.SaveChangesAsync(cts);

                    var entity = await context.ContactRequests.FirstOrDefaultAsync(x => x.Email == cReq.Email, cts);

                    if (entity != null)
                        return new ContactResult { Status = "200", ContactRequest = ContactFactory.Create(entity) };
                    else
                        return new ContactResult { Status = "500", Error = "New contact request not created" };
                }
                else
                {
                    return new ContactResult { Status = "409", Error = "Conflict" };
                }

            }
            catch (Exception ex) { return new ContactResult { Status = "500", Error = ex.Message }; }
        }

        return new ContactResult { Status = "400", Error = "Bad request" };
    }
}