using Api.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.EF.Infrastructure;
using Domain.Abstractions.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Services
{
    public class CustomerDataService : ICustomerDataService
    {
        private readonly ICustomerContext _customerContext;
        private readonly IMapper _mapper;

        public CustomerDataService(ICustomerContext customerContext, IMapper mapper)
        {
            _customerContext = customerContext ?? throw new ArgumentNullException(nameof(customerContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<Customer>> ListCustomersAsync()
        {
            return await _customerContext.Customers
                .ProjectTo<Customer>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var customer = await _customerContext.Customers
                .Where(c => c.Id == id).FirstOrDefaultAsync();

            if (customer != null)
            {
                return _mapper.Map<Customer>(customer);
            }

            throw new ItemNotFoundException();
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            var dbCustomer = new Data.EF.Models.Customer
            {
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone
            };
            var newDbCustomer = await _customerContext.Customers.AddAsync(dbCustomer);

            int? result = null;
            result = await _customerContext.SaveChangesAsync();

            if (result.HasValue && result.Value == 1)
            {
                return _mapper.Map<Customer>(dbCustomer);
            }

            throw new InternalServerErrorException();
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var dbCustomer = await _customerContext.Customers
                .Where(c => c.Id == customer.Id).FirstOrDefaultAsync();
            
            if (dbCustomer != null)
            {
                dbCustomer.Email = customer.Email;
                dbCustomer.Phone = customer.Phone;
                dbCustomer.Name = customer.Name;
            }

            int? result = null;
            result = await _customerContext.SaveChangesAsync();

            if (result.HasValue && result.Value == 1)
            {
                return _mapper.Map<Customer>(dbCustomer);
            }

            throw new InternalServerErrorException();
        }

        public async Task<int> DeleteCustomer(int id)
        {
            var dbCustomer = new Data.EF.Models.Customer { Id = id };

            _customerContext.Customers.Attach(dbCustomer);
            _customerContext.Customers.Remove(dbCustomer);

            return await _customerContext.SaveChangesAsync();
        }
    }
}
