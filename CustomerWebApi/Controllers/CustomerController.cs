using CustomerWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _customerDbContext;

        public CustomerController(CustomerDbContext customerDbContext)
        {
            this._customerDbContext = customerDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return _customerDbContext.Customers;

        }

        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetCustomerByID(int customerID)
        {
            var customer = await _customerDbContext.Customers.FindAsync(customerID);
            return customer;

        }

        [HttpPost]
        public async Task<ActionResult> Create(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            await _customerDbContext.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Customer customer)
        {
             _customerDbContext.Customers.Update(customer);
            await _customerDbContext.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpDelete("{customerId:int}")]
        public async Task<ActionResult> Delete(int customerId)
        {
            var customer = await _customerDbContext.Customers.FindAsync(customerId);
            _customerDbContext.Customers.Remove(customer);
            await _customerDbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
