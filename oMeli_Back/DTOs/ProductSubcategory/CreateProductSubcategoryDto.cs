namespace oMeli_Back.DTOs.ProductSubcategory;

public record CreateProductSubcategoryDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
}
