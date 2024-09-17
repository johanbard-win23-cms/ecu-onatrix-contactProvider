using Microsoft.AspNetCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ecu_onatrix_contactProvider.Data.Entities;

namespace ecu_onatrix_contactProvider.Data.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext<ContactEntity>(options)
    {
       
    }
}
