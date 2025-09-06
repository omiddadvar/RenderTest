namespace RenderTest.DTOs.Products;

public class CreateProductDTO
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; } = 0;
    public int CategoryId { get; set; }
}