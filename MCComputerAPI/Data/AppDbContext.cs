
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
}