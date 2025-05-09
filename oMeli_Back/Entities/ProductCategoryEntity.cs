namespace oMeli_Back.Entities;

public class ProductCategoryEntity
{
    public Guid Id { get; set; }
    public String Name { get; set; } = String.Empty;
    public String Description { get; set; } = String.Empty;
    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
}
