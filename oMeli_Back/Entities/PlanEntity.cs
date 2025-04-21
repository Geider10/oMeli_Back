namespace oMeli_Back.Entities
{
    public class PlanEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PublicationLimited { get; set; }
        public bool PublicationCustom { get; set; }
        public bool StoreCustom { get; set; }
        public bool PublicationUnlimited { get; set; }
        public bool ViewStatics { get; set; }
        public bool CSVImport { get; set; }
    }
}
