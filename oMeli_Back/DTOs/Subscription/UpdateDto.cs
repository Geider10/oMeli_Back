namespace oMeli_Back.DTOs.Subscription
{
    public class UpdateDto
    {
        public string SubscriptionId { get; set; }
        public string PlanId { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string State { get; set; }
        public bool Renovation { get; set; }
    }
}
