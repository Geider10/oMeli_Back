namespace oMeli_Back.DTOs.ProductSubcategory;

public record ReturnSimpleProductSubcategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}