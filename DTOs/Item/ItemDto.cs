namespace Catalog.DTOs.Item;

public class ItemDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }
}
