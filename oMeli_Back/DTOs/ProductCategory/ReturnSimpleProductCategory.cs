namespace oMeli_Back.DTOs.ProductCategory;

public record ReturnSimpleProductCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}