using Catalog.Models;

namespace Catalog.Services.Interfaces;

public interface IItemService
{
    Task<IEnumerable<Item>> GetItemsAsync(int? categoryId, int page, int pageSize, CancellationToken cancellationToken);

    Task<Item> CreateItemAsync(Item item, CancellationToken cancellationToken);

    Task UpdateItemAsync(Item item, CancellationToken cancellationToken);

    Task DeleteItemAsync(int itemId, CancellationToken cancellationToken);
}
