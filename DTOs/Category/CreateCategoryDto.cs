namespace Catalog.DTOs.Category;

public class CreateCategoryDto
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
