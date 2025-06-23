using AutoMapper;
using Catalog.DTOs.Item;
using Catalog.Models;
using Catalog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly IMapper _mapper;

    public ItemsController(IItemService itemService, IMapper mapper)
    {
        _itemService = itemService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns a paginated list of items, optionally filtered by category.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ItemDto>), StatusCodes.Status200OK)]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Get items", Description = "Retrieves a list of items. Supports optional filtering by category and pagination.")]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems(
        [FromQuery] int? categoryId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var items = await _itemService.GetItemsAsync(categoryId, page, pageSize, cancellationToken);
        var dto = _mapper.Map<IEnumerable<ItemDto>>(items);

        return Ok(dto);
    }

    /// <summary>
    /// Get items by category via RESTful route.
    /// </summary>
    [HttpGet("~/api/categories/{categoryId}/items")]
    [ProducesResponseType(typeof(IEnumerable<ItemDto>), StatusCodes.Status200OK)]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Get items by category", Description = "RESTful access to items under a specific category")]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsByCategory(
        int categoryId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var items = await _itemService.GetItemsAsync(categoryId, page, pageSize, cancellationToken);
        var dto = _mapper.Map<IEnumerable<ItemDto>>(items);
        return Ok(dto);
    }

    /// <summary>
    /// Creates a new item.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ItemDto), StatusCodes.Status201Created)]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Create item", Description = "Creates a new item under a given category.")]
    public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto createDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Item>(createDto);
        var created = await _itemService.CreateItemAsync(item, cancellationToken);
        var resultDto = _mapper.Map<ItemDto>(created);

        return Created("", resultDto);
    }

    /// <summary>
    /// Updates an existing item by ID.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation(Summary = "Update item", Description = "Updates an item’s details by its ID.")]
    public async Task<IActionResult> UpdateItem(int id, UpdateItemDto updateDto, CancellationToken cancellationToken)
    {
        if (id != updateDto.Id)
            return BadRequest();

        var item = _mapper.Map<Item>(updateDto);
        await _itemService.UpdateItemAsync(item, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Deletes an item by ID.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation(Summary = "Delete item", Description = "Deletes a specific item by its ID.")]
    public async Task<IActionResult> DeleteItem(int id, CancellationToken cancellationToken)
    {
        await _itemService.DeleteItemAsync(id, cancellationToken);

        return NoContent();
    }
}