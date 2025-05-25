namespace oMeli_Back.Entities;

public class ProductSubcategoryEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ProductCategoryId { get; set; }
    public ProductCategoryEntity ProductCategory { get; set; } = null!;
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
}
