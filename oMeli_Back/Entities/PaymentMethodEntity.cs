namespace oMeli_Back.Entities
{
    public class PaymentMethodEntity
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime DateCreation { get; set; }

        public StoreEntity Store { get; set; }
        public PaymentMethodEntity()
        {
            Id = Guid.NewGuid();
            DateCreation = DateTime.Now;
        }
    }
}
