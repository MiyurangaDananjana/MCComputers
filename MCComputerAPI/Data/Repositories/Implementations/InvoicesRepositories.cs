using MCComputerAPI.Data.Interfaces;
using MCComputerAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MCComputerAPI.Data.Implementations
{
    public class InvoicesRepositories : IInvoicesRepositories
    {
        private readonly AppDbContext _context;

        public InvoicesRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> AddInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
                return false;

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _context.Invoices
                                 .Include(i => i.InvoiceItems)
                                 .ToListAsync();
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(int id)
        {
            return await _context.Invoices
                                 .Include(i => i.InvoiceItems)
                                 .FirstOrDefaultAsync(i => i.InvoiceId == id);
        }

        public async Task<bool> UpdateInvoiceAsync(Invoice invoice)
        {
            var existingInvoice = await _context.Invoices
                                                .Include(i => i.InvoiceItems)
                                                .FirstOrDefaultAsync(i => i.InvoiceId == invoice.InvoiceId);

            if (existingInvoice == null)
                return false;

            existingInvoice.CustomerId = invoice.CustomerId;
            existingInvoice.CreateDate = invoice.CreateDate;
            existingInvoice.TotalAmmount = invoice.TotalAmmount;

            _context.Invoices.Update(existingInvoice);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
