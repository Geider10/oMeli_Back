namespace oMeli_Back.Entities
{
    public class PlanEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PublicationLimit { get; set; }
        public bool ImportCSV { get; set; }
        public bool StoreCustom { get; set; }
        public bool ViewStatics { get; set; }
    }
}
