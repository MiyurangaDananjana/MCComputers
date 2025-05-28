using MCComputerAPI.Data.Interfaces;
using MCComputerAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MCComputerAPI.Data.Implementations;

public class ProductRepositories : IProductsRepositories
{

    private readonly AppDbContext _context;

    public ProductRepositories(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.ProductId);
        if (existingProduct == null)
            return false;

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Stock = product.Stock;

        _context.Products.Update(existingProduct);
        await _context.SaveChangesAsync();
        return true;
    }
}