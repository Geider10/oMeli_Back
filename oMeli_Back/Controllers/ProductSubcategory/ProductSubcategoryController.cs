using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.DTOs.ProductSubcategory;
using oMeli_Back.Services.ProductSubcategory;
using oMeli_Back.Utils;

namespace oMeli_Back.Controllers.ProductSubcategory;

[ApiController]
[Route("api/[controller]")]
public class ProductSubcategoryController : ControllerBase
{
    private readonly ProductSubcategoryService _service;

    public ProductSubcategoryController(ProductSubcategoryService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateSubcategory([FromBody] CreateProductSubcategoryDto dto)
    {
        Guid userId = this.ExtractUserIdOrThrow();
        return await HandleOperationAsync(async () =>
        {
            var res = await _service.CreateAsync(dto, userId);
            return StatusCode(201, new { ok = true, data = res });
        });
    }

    [HttpPatch("{subcatId:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateSubcategory(
        [FromBody] UpdateProductSubcategoryDto dto,
        [FromRoute] Guid subcatId
    )
    {
        Guid userId = this.ExtractUserIdOrThrow();
        return await HandleOperationAsync(async () =>
        {
            bool res = await _service.UpdateAsync(dto, subcatId, userId);
            return Ok(new { ok = true });
        });
    }

    private Guid ExtractUserIdOrThrow()
    {
        var userId = User.GetUserId();
        if (userId == null)
            throw new UnauthorizedAccessException("Invalid token");
        return userId.Value;
    }

    private async Task<IActionResult> HandleOperationAsync(Func<Task<IActionResult>> operation)
    {
        try
        {
            return await operation();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { ok = false, message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { ok = false, message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { ok = false, message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { ok = false, message = "Internal Server Error" });
        }
    }
}
