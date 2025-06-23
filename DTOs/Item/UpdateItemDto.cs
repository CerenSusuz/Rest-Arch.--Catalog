namespace Catalog.DTOs.Item;

public class UpdateItemDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int CategoryId { get; set; }
}
