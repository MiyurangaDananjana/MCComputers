using MCComputerAPI.Data.Interfaces;
using MCComputerAPI.Models.DTOs;
using MCComputerAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCComputerAPI;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsRepositories _productRepository;

    public ProductsController(IProductsRepositories productRepository)
    {
        _productRepository = productRepository;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return Ok(products);
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }


        return Ok(product);
    }

    // POST: api/Products
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDTOs productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Product product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Stock = productDto.Stock
        };

        var createdProduct = await _productRepository.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTOs productDto)
    {
        if (id != productDto.ProductId)
        {
            return BadRequest("Product ID mismatch.");
        }

        Product product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Stock = productDto.Stock
        };

        var result = await _productRepository.UpdateProductAsync(product);
        if (!result)
        {
            return NotFound();
        }


        return NoContent();
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productRepository.DeleteProductAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}