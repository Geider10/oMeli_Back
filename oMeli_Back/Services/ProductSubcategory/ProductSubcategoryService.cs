using Mapster;
using Microsoft.EntityFrameworkCore;
using oMeli_Back.Context;
using oMeli_Back.DTOs.ProductSubcategory;
using oMeli_Back.Entities;

namespace oMeli_Back.Services.ProductSubcategory;

public class ProductSubcategoryService
{
    private readonly AppDBContext _context;

    private readonly string SubcatDefaultname = "General";

    public ProductSubcategoryService(AppDBContext context)
    {
        _context = context;
    }

    private async Task<bool> ExistsInCategoryByNameAsync(
        Guid categoryId,
        string name,
        Guid? excludedSubcategoryId = null
    )
    {
        string normalizedName = name.Trim().ToLower();

        return await _context.ProductSubcategories.AnyAsync(subcat =>
            subcat.Name.Trim().ToLower() == normalizedName
            && subcat.ProductCategoryId == categoryId
            && (excludedSubcategoryId == null || subcat.Id != excludedSubcategoryId)
        );
    }

    private async Task<ProductSubcategoryEntity> GetByIdAsync(Guid subcatId)
    {
        var prod = await _context.ProductSubcategories.FindAsync(subcatId);
        if (prod == null)
            throw new KeyNotFoundException("Subcategory not found");
        return prod;
    }

    public async Task<ProductSubcategoryEntity> CreateAsync(
        CreateProductSubcategoryDto dto,
        Guid userId
    )
    {
        try
        {
            bool exists = await this.ExistsInCategoryByNameAsync(dto.CategoryId, dto.Name, null);
            if (!exists)
                throw new InvalidOperationException(
                    $"La categoría {dto.Name} ya existe en la categoría {dto.CategoryId}"
                );

            var subcategory = new ProductSubcategoryEntity
            {
                Id = Guid.NewGuid(),
                Name = dto.Name.Trim(),
                CreatedBy = userId,
                Description = dto.Description,
                ProductCategoryId = dto.CategoryId,
            };
            var newSubcategory = _context.ProductSubcategories.Add(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<bool> UpdateAsync(
        UpdateProductSubcategoryDto dto,
        Guid subcategoryId,
        Guid userId
    )
    {
        try
        {
            var subcategory = await this.GetByIdAsync(subcategoryId);

            if (subcategory.Name == SubcatDefaultname)
            {
                throw new InvalidOperationException("No puede modificar esta categoría");
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                bool existsName = await this.ExistsInCategoryByNameAsync(
                    subcategory.ProductCategoryId,
                    dto.Name,
                    subcategoryId
                );
                if (existsName)
                    throw new InvalidOperationException(
                        "Una subcategoría con ese nombre ya existe en esta categoría"
                    );
            }

            dto.Adapt(
                subcategory,
                TypeAdapterConfig<UpdateProductSubcategoryDto, ProductSubcategoryEntity>
                    .NewConfig()
                    .IgnoreNullValues(true)
                    .Config
            );
            subcategory.UpdatedBy = userId;
            subcategory.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
