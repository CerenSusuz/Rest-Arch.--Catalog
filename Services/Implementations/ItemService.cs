using Catalog.Database.DbContexts;
using Catalog.Models;
using Catalog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Services.Implementations;

public class ItemService : IItemService
{
    private readonly CatalogDbContext _context;

    public ItemService(CatalogDbContext context) => _context = context;

    public async Task<IEnumerable<Item>> GetItemsAsync(int? categoryId, int page, int pageSize, CancellationToken cancellationToken)
    {
        var query = _context.Items.AsQueryable();

        if (categoryId.HasValue)
            query = query.Where(i => i.CategoryId == categoryId.Value);

        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<Item> CreateItemAsync(Item item, CancellationToken cancellationToken)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync(cancellationToken);

        return item;
    }

    public async Task UpdateItemAsync(Item item, CancellationToken cancellationToken)
    {
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteItemAsync(int itemId, CancellationToken cancellationToken)
    {
        var item = await _context.Items.FindAsync([itemId], cancellationToken);
        
        if (item != null)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
