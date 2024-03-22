using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Infrastructure
{
    public interface ICustomerDataService
    {
        Task<List<Customer>> ListCustomersAsync();
        Task<Customer> GetCustomerById(int id);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<int> DeleteCustomer(int id);
    }
}
