using Catalog.Database.DbContexts;
using Catalog.Models;
using Catalog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly CatalogDbContext _context;

    public CategoryService(CatalogDbContext context) => _context = context;

    public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        return await _context.Categories.Include(c => c.Items).ToListAsync(cancellationToken);
    }

    public async Task<Category> CreateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task UpdateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
    
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
