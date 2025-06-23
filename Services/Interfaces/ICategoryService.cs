using Catalog.Models;

namespace Catalog.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken);

    Task<Category> CreateCategoryAsync(Category category, CancellationToken cancellationToken);

    Task UpdateCategoryAsync(Category category, CancellationToken cancellationToken);

    Task DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken);
}