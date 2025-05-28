
using MCComputerAPI.Data.Interfaces;
using MCComputerAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCComputerAPI;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{

    private readonly ICustomersRepositories _customersRepositories;

    public CustomersController(ICustomersRepositories customersRepositories)
    {
        _customersRepositories = customersRepositories;
    }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var customers = await _customersRepositories.GetAllCustomersAsync();
            return Ok(customers);
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _customersRepositories.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            var created = await _customersRepositories.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = created.CustomerId }, created);
        }

        // PUT: api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
                return BadRequest("ID mismatch.");

            var updated = await _customersRepositories.UpdateCustomerAsync(customer);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customersRepositories.DeleteCustomerAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

}