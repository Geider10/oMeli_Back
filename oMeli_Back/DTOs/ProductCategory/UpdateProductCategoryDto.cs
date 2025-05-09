namespace oMeli_Back.DTOs.ProductCategory;

public record UpdateProductCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
