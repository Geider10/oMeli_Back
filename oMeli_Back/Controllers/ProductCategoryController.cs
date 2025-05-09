using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.DTOs.ProductCategory;
using oMeli_Back.Entities;
using oMeli_Back.Utils;
using oMeli_Back.Validators.ProductCategory;

namespace oMeli_Back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCategoryController : ControllerBase
{
    private readonly ProductCategoryService _service;

    public ProductCategoryController(ProductCategoryService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(ProductCategoryEntity), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductCategoryDto dto)
    {
        try
        {
            var userId = User.GetUserId();
            if (userId == null)
                return Unauthorized(new { message = "Invalid token" });

            var internalDto = new StoreProductCategoryDto
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedBy = userId.Value,
            };
            var entity = await _service.CreateAsync(internalDto);

            return Ok("Creado exitosamente");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { ok = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error" });
        }

    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductCategoryEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        try
        {
            var category = await _service.GetOneByIdAsync(id);
            return Ok(category);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPublic()
    {
        try
        {
            var categories = await _service.GetAllPublicAsync();
            return Ok(categories);
        }
        catch (System.Exception)
        {
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    [HttpGet("admin")]
    [ProducesResponseType(typeof(IEnumerable<ProductCategoryEntity>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAdmin()
    {
        try
        {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }
        catch (System.Exception)
        {
            return StatusCode(500, new { message = "Ha ocurrido un error inesperado." });
        }
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCategoryDto dto)
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized(new { message = "Invalid token" });

        try
        {
            var validator = new UpdateProductCategoryDtoValidator();
            var validationResult = await validator.ValidateAsync(dto);
            
            if (!validationResult.IsValid)
            {
                return BadRequest(new { ok = false, errors = validationResult.Errors });
            }

            var success = await _service.UpdateAsync(id, dto, userId.Value);
            return Ok(new { ok = true });

        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { ok = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ha ocurrido un error inesperado." });
        }
    }
}
