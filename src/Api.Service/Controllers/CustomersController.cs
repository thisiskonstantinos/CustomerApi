using Api.Models;
using Domain.Abstractions;
using Domain.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Service.Controllers
{
    /// <summary>
    /// The Custoemrs Controller for a basic CRUD .NET API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;

        public CustomersController(
            ILogger<CustomersController> logger,
            ICustomerService customerService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        /// <summary>
        /// This endpoint lists all Customers
        /// </summary>
        /// <returns>Returns OK with the results</returns>
        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            _logger.LogInformation($"Entering {nameof(ListAll)}");

            try
            {
                var customers = await _customerService.ListCustomers();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                // needs refactoring to wrap the error with the status code
                throw new ItemNotFoundException(ex.Message);
            }            
        }

        /// <summary>
        /// This endpoint return a Customer by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns OK with the result</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Entering {nameof(GetById)}");

            try
            {
                // TO-DO: Add Validator for input validation

                var customer = await _customerService.GetCustomerById(id);
                return Ok(customer);
            } 
            catch (ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// This endpoint creates a new Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Returns 201 with the result or 400 HTTP codes</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            _logger.LogInformation($"Entering {nameof(Create)}");

            // TO-DO: Add Validator for input validation

            try
            {
                var createdCustomer = await _customerService.AddCustomer(customer);
                return CreatedAtAction(nameof(GetById), new { id = createdCustomer.Id }, createdCustomer);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This endpoint updates a Customer by ID
        /// </summary>
        /// <param name="customer">The Customer object</param>
        /// <returns>Returns 204 if updated, or HTTP codes 400/404</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Customer customer)
        {
            _logger.LogInformation($"Entering {nameof(Update)}");

            try
            {
                var result = await _customerService.UpdateCustomer(customer);

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This endpoint deletes a Customer by ID
        /// </summary>
        /// <param name="id">The Customer ID</param>
        /// <returns>Returns 204 if deleted or 404 HTTP code</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Entering {nameof(Delete)}");

            try
            {
                await _customerService.DeleteCustomer(id);
                return NoContent();
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
            
        }
    }
}
