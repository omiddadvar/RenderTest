using Microsoft.AspNetCore.Mvc;
using RenderTest.Data;
using RenderTest.Data.Entities;
using RenderTest.DTOs.Categories;
using RenderTest.DTOs.Results;

namespace RenderTest.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController(MainDBContext context) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(
    [FromRoute] int id,
    CancellationToken cancellationToken)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id);
        if (category is null)
        {
            throw new NullReferenceException("Category Does Not Exists");
        }
        return Ok(new SuccessResult
        {
            Data = category,
            Message = "Category Fetched Successfully"
        });
    }
    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var categories = context.Categories.ToArray();
        if (categories is null)
        {
            throw new NullReferenceException("There Is No Category");
        }
        return Ok(new SuccessResult
        {
            Data = categories,
            Message = "Categories Fetched Successfully"
        });
    }
    [HttpPost]
    public async Task<IActionResult> AddCategory(
        [FromBody] CreateCategoryDTO input, 
        CancellationToken cancellationToken)
    {
        var category = new Category { Name = input.Name };
        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Ok(new SuccessResult
        {
            Data = category,
            Message = "Category Created Successfully"
        });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(
        [FromRoute] int id,
        [FromBody] UpdateCategoryDTO input, 
        CancellationToken cancellationToken)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id);
        if (category is null)
        {
            throw new NullReferenceException("Category Does Not Exists");
        }
        category.Name = input.Name;
        await context.SaveChangesAsync(cancellationToken);
        return Ok(new SuccessResult { 
            Data = category , 
            Message = "Category Updated Successfully"
        });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id);
        if (category is null)
        {
            throw new NullReferenceException("Category Does Not Exists");
        }
        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
        return Ok(new SuccessResult
        {
            Data = id,
            Message = "Category Deleted Successfully"
        });
    }
}
