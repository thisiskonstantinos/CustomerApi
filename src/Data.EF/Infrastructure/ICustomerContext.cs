using Data.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.EF.Infrastructure
{
    public interface ICustomerContext : IDbContext
    {
        DbSet<Customer> Customers { get; set; }
    }
}
