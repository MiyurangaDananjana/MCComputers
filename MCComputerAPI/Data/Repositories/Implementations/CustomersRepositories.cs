using MCComputerAPI.Data.Interfaces;
using MCComputerAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace MCComputerAPI.Data.Implementations
{
    public class CustomersRepositories : ICustomersRepositories
    {
        private readonly AppDbContext _context;

        public CustomersRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Log.Error($"An error occurred while retrieving customers: {ex.Message}");

                // Return an empty list or rethrow the exception depending on your needs
                return Enumerable.Empty<Customer>();
                // OR throw; // If you prefer to let the caller handle the exception
            }
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers
                                 .Include(c => c.Invoices)
                                 .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var existing = await _context.Customers.FindAsync(customer.CustomerId);
            if (existing == null) return false;

            existing.Name = customer.Name;
            existing.Email = customer.Email;
            existing.Phone = customer.Phone;

            _context.Customers.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
