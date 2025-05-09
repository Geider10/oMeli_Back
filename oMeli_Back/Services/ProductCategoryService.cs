using Mapster;
using Microsoft.EntityFrameworkCore;
using oMeli_Back.Context;
using oMeli_Back.DTOs.ProductCategory;
using oMeli_Back.Entities;

public class ProductCategoryService
{
    private readonly AppDBContext _context;

    public ProductCategoryService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        var exists = await _context.ProductCategories.AnyAsync(c =>
            c.Name.ToLower() == name.ToLower()
        );
        return exists;
    }

    public async Task<ProductCategoryEntity> CreateAsync(StoreProductCategoryDto dto)
    {
        var existing = await this.ExistsByNameAsync(dto.Name);
        if (existing)
        {
            throw new InvalidOperationException("A category with this name already exists.");
        }

        var category = new ProductCategoryEntity
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            CreatedBy = dto.CreatedBy,
        };

        _context.ProductCategories.Add(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<ProductCategoryEntity> GetOneByIdAsync(Guid id)
    {
        var category = await _context.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);
        if (category is null)
        {
            throw new KeyNotFoundException($"Category with ID {id} was not found.");
        }
        return category;
    }

    public async Task<IEnumerable<ProductCategoryEntity>> GetAllAsync()
    {
        try
        {
            return await _context.ProductCategories.AsNoTracking().ToListAsync();
        }
        catch (System.Exception)
        {
            throw new ApplicationException();
        }
    }

    public async Task<IEnumerable<ReturnSimpleProductCategoryDto>> GetAllPublicAsync()
    {
        try
        {
            return await _context.ProductCategories
                .AsNoTracking()
                .Where(cat => cat.IsActive)
                .ProjectToType<ReturnSimpleProductCategoryDto>()
                .ToListAsync();
        }
        catch (System.Exception)
        {
            throw new ApplicationException();
        }
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateProductCategoryDto dto, Guid userId)
    {
        try
        {
            var category = await this.GetOneByIdAsync(id);

            if (!string.IsNullOrEmpty(dto.Name) && dto.Name != category.Name)
            {
                bool nameExists = await this.checkNameAtUpdate(dto.Name, id);
                if (nameExists)
                {
                    throw new InvalidOperationException("A category with this name already exists.");
                }
            }

            dto.Adapt(category, TypeAdapterConfig<UpdateProductCategoryDto, ProductCategoryEntity>.NewConfig()
                .IgnoreNullValues(true)
                .Config);

            category.UpdatedDate = DateTime.UtcNow;
            category.UpdatedBy = userId;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    private async Task<bool> checkNameAtUpdate(string name, Guid id)
    {
        var nameExists = await _context.ProductCategories
            .AnyAsync(c => c.Name.ToLower() == name.ToLower() && c.Id != id);
        return nameExists;
    }
}
