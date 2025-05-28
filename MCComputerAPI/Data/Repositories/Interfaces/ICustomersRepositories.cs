
using MCComputerAPI.Models.Entities;

namespace MCComputerAPI.Data.Interfaces;

public interface ICustomersRepositories
{
    Task<Customer> AddCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
}