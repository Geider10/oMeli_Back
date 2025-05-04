namespace oMeli_Back.DTOs.Store
{
    public class CreateScheduleDto
    {
        public string StoreId { get; set; }
        public string Day { get; set; }
        public string HourStart { get; set; }
        public string HourEnd { get; set; }
    }
}
