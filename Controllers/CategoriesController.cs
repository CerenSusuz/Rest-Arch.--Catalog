using AutoMapper;
using Catalog.DTOs.Category;
using Catalog.Models;
using Catalog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns a list of all categories.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Get all categories", Description = "Retrieves a list of all available categories.")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories(CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetCategoriesAsync(cancellationToken);
        var dto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

        return Ok(dto);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Create category", Description = "Creates a new category and returns it.")]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createDto, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(createDto);
        var created = await _categoryService.CreateCategoryAsync(category, cancellationToken);
        var resultDto = _mapper.Map<CategoryDto>(created);

        return CreatedAtAction(nameof(GetCategories), new { id = resultDto.Id }, resultDto);
    }

    /// <summary>
    /// Updates an existing category by ID.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation(Summary = "Update category", Description = "Updates the details of a category by its ID.")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateDto, CancellationToken cancellationToken)
    {
        if (id != updateDto.Id)
            return BadRequest();

        var category = _mapper.Map<Category>(updateDto);
        await _categoryService.UpdateCategoryAsync(category, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Deletes a category and its related items.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation(Summary = "Delete category", Description = "Deletes a category by its ID, along with related items.")]
    public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteCategoryAsync(id, cancellationToken);

        return NoContent();
    }
}