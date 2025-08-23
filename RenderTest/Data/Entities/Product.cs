using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using RenderTest.Abstractions.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenderTest.Data.Entities;

public class Product : IEntity
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(100), Unicode]
    public string Name { get; set; }
    [Required, StringLength(2000), Unicode]
    public string? Description { get; set; }
    public decimal Price { get; set; } = 0;

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
