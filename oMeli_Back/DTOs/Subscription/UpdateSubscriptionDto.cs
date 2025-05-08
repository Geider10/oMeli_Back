namespace oMeli_Back.DTOs.Subscription
{
    public class UpdateSubscriptionDto
    {
        public string PlanId { get; set; }
        public string DataStart { get; set; }
        public string DateEnd { get; set; }
        public string State { get; set; }
        public bool Renovation { get; set; }
    }
}
