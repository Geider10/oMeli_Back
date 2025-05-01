namespace oMeli_Back.DTOs.Subscription
{
    public class CreateDto
    {
        public string UserId { get; set; }
        public string PlanId { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string State { get; set; }
        public bool Renovation { get; set; }
    }
}
