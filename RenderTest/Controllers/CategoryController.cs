using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RenderTest.Abstractions.Services;
using RenderTest.Data;
using RenderTest.Data.Entities;
using RenderTest.DTOs.Categories;
using RenderTest.DTOs.Results;
using StackExchange.Redis;

namespace RenderTest.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController(
        MainDBContext context,
        IRedisService redis
    ) : ControllerBase
{
    private const string REDIS_KEY = "Categories";

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

    [HttpPut("/update-redis")]
    public async Task<IActionResult> UpdateCategoriesInRedis(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var categories = context.Categories.ToList();
        if (categories is null)
        {
            throw new NullReferenceException("Category Does Not Exists");
        }
        await redis.SetAsync<IEnumerable<Category>>(REDIS_KEY, categories);
        return Ok(new SuccessResult
        {
            Data = null,
            Message = "Category Fetched Successfully"
        });
    }

    [HttpGet("/read-redis")]
    public async Task<IActionResult> GetCategoriesInRedis(
    [FromRoute] int id,
    CancellationToken cancellationToken)
    {
        if (!await redis.ExistsAsync(REDIS_KEY))
        {
            throw new NullReferenceException("Category Data Does Not Exists In Redis");
        }
        var categories = await redis.GetAsync<IEnumerable<Category>>(REDIS_KEY);
        return Ok(new SuccessResult
        {
            Data = categories,
            Message = "Category Fetched Successfully"
        });
    }
}
