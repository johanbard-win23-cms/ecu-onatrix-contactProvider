using ecu_onatrix_contactProvider.Data.Entities;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories;

public class ContactFactory
{
    public static ContactEntity Create(ContactModel model)
    {
        return new ContactEntity
        {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            Category = model.Category
        };
    }

    public static ContactModel Create(ContactEntity entity)
    {
        return new ContactModel
        {
            Id = entity.Id,
            Name = entity.Name!,
            Email = entity.Email,
            Phone = entity.Phone!,
            Category = entity.Category!
        };
    }
}
