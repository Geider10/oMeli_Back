namespace oMeli_Back.DTOs.Subscription
{
    public class UpdateDto
    {
        public string PlanId { get; set; }
        public string DataStart { get; set; }
        public string DateEnd { get; set; }
        public string State { get; set; }
        public bool Renovation { get; set; }
    }
}
