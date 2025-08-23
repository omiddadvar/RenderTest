using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using RenderTest.Abstractions.Data;

namespace RenderTest.Data.Entities;

public class Category : IEntity
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(100) , Unicode]
    public string Name { get; set; }
}
