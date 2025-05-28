
using MCComputerAPI.Models.Entities;

namespace MCComputerAPI.Data.Interfaces;

public interface IInvoicesRepositories
{
    // Create
    Task<Invoice> AddInvoiceAsync(Invoice invoice);

    // Read
    Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
    Task<Invoice?> GetInvoiceByIdAsync(int id);

    // Update
    Task<bool> UpdateInvoiceAsync(Invoice invoice);

    // Delete
    Task<bool> DeleteInvoiceAsync(int id);
}