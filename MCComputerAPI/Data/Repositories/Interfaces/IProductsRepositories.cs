
using MCComputerAPI.Models.Entities;

namespace MCComputerAPI.Data.Interfaces;

public interface IProductsRepositories
{

    // Create
    Task<Product> AddProductAsync(Product product);

    // Read
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);

    // Update
    Task<bool> UpdateProductAsync(Product product);

    // Delete
    Task<bool> DeleteProductAsync(int id);


}