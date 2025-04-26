namespace oMeli_Back.Entities
{
    public class ScheduleEntity
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string Day { get; set; }
        public string HourStart { get; set; }
        public string HourEnd { get; set; }
        public DateTime DateCreation { get; set; }

        public StoreEntity Store { get; set; }
        public ScheduleEntity()
        {
            Id = Guid.NewGuid();
            DateCreation = DateTime.Now;
        }
    }
}
