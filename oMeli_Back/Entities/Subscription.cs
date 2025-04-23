namespace oMeli_Back.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; } //referncia a la entidad relacionada
        public Guid PlanId { get; set; }
        public PlanEntity Plan { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateCreation { get; set; }
        public string State { get; set; } // activa, inactiva, pausada
        public bool Renovation { get; set; }

        public Subscription()
        {
            Id = Guid.NewGuid();
            DateCreation = DateTime.Now;
        }
    }
}
