using Microsoft.AspNetCore.Mvc;
using RenderTest.Data;
using RenderTest.Data.Entities;
using RenderTest.DTOs.Products;
using RenderTest.DTOs.Results;

namespace RenderTest.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController(MainDBContext context) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var product = context.Products.FirstOrDefault(c => c.Id == id);
        if (product is null)
        {
            throw new NullReferenceException("Product Does Not Exists");
        }
        return Ok(new SuccessResult
        {
            Data = product,
            Message = "Product Fetched Successfully"
        });
    }
    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var products = context.Products.ToArray();
        if (products is null)
        {
            throw new NullReferenceException("There Is No Product");
        }
        return Ok(new SuccessResult
        {
            Data = products,
            Message = "Products Fetched Successfully"
        });
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct(
        [FromBody] CreateProductDTO input,
        CancellationToken cancellationToken)
    {
        var product = new Product { 
            Name = input.Name,
            CategoryId = input.CategoryId,
            Description = input.Description,
            Price = input.Price
        };
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Ok(new SuccessResult
        {
            Data = product,
            Message = "Product Created Successfully"
        });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] int id,
        [FromBody] UpdateProductDTO input,
        CancellationToken cancellationToken)
    {
        var product = context.Products.FirstOrDefault(c => c.Id == id);
        if (product is null)
        {
            throw new NullReferenceException("Product Does Not Exists");
        }
        product.Name = input.Name;
        product.CategoryId = input.CategoryId;
        product.Description = input.Description;
        product.Price = input.Price;

        await context.SaveChangesAsync(cancellationToken);
        return Ok(new SuccessResult
        {
            Data = product,
            Message = "Product Updated Successfully"
        });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var product = context.Products.FirstOrDefault(c => c.Id == id);
        if (product is null)
        {
            throw new NullReferenceException("Product Does Not Exists");
        }
        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
        return Ok(new SuccessResult
        {
            Data = id,
            Message = "Product Deleted Successfully"
        });
    }
}
