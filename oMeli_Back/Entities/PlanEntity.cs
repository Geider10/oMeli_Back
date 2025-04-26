namespace oMeli_Back.Entities
{
    public class PlanEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool StoreCreate { get; set; }
        public int ProductLimited { get; set; }
        public bool PublicationCustom { get; set; }
        public bool StoreCustom { get; set; }
        public bool ViewStatics { get; set; }
        public bool CSVImport { get; set; }
        public bool Priority { get; set; }

        public ICollection<SubscriptionEntity> Subscriptions { get; set; } 
    }
}
