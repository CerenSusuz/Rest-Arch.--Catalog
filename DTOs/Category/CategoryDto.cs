using Catalog.DTOs.Item;

namespace Catalog.DTOs.Category;

public class CategoryDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public List<ItemDto>? Items { get; set; }
}
