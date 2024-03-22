using Api.Models;
using Data.EF.Infrastructure;
using Domain.Abstractions;

namespace Domain.Logic
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDataService _customerDataService;

        public CustomerService(ICustomerDataService customerDataService)
        {
            _customerDataService = customerDataService ?? throw new ArgumentNullException(nameof(customerDataService));
        }

        public Task<List<Customer>> ListCustomers()
        {
            return _customerDataService.ListCustomersAsync();
        }

        public Task<Customer> GetCustomerById(int id)
        {
            return _customerDataService.GetCustomerById(id);
        }

        public Task<Customer> AddCustomer(Customer customer)
        {
            return _customerDataService.AddCustomer(customer);
        }

        public Task<Customer> UpdateCustomer(Customer customer)
        {
            return _customerDataService.UpdateCustomer(customer);
        }

        public Task DeleteCustomer(int id)
        {
            return _customerDataService.DeleteCustomer(id);
        }
    }
}
