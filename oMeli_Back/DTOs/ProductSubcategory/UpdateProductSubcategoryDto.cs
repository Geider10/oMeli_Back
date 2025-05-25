namespace oMeli_Back.DTOs.ProductSubcategory;

public record UpdateProductSubcategoryDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
}
