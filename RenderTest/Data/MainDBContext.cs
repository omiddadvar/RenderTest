using Microsoft.EntityFrameworkCore;
using RenderTest.Data.Entities;

namespace RenderTest.Data;

public class MainDBContext : DbContext
{
    public MainDBContext(DbContextOptions options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}
