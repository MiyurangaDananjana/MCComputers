
using Microsoft.EntityFrameworkCore;
using MCComputerAPI.Models.Entities;

namespace MCComputerAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
    {
    }
    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<InvoiceItems> InvoiceItems { get; set; } 
}