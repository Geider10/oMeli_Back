namespace oMeli_Back.Entities
{
    public class StoreEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public string Name { get; set; }
        public string Wassap { get; set; }
        public string Mail { get; set; }
        public bool Local { get; set; }
        public string Address { get; set; }
        public string AddressDescription { get; set; }
        public string LocalNumber { get; set; }
        public bool Shipping { get; set; }
        public bool Meeting { get; set; }
        public int CurrentProducts { get; set; }
        public DateTime DateCreation { get; set; }
        //relations
        public UserEntity User { get; set; }
        public SubscriptionEntity Subscription { get; set; }
        public ICollection<ScheduleEntity> Schedules { get; set; }
        public ICollection<PaymentMethodEntity> PaymentMethods { get; set; }
        public ICollection<FollowerEntity> Followers { get; set; }
        public ICollection<ImageEntity> Images { get; set; }
        public StoreEntity()
        {
            Id = Guid.NewGuid();
            DateCreation = DateTime.Now;
        }
    }
}
