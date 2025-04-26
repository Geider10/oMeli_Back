namespace oMeli_Back.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Date_Creation { get; set; }
        //representa relacion y permite navegar entre las tablas
        public ICollection<UserRoleEntity> UserRoles { get; set; }
        public SubscriptionEntity Subscription { get; set; }

        public UserEntity()
        {
            Id = Guid.NewGuid();
            Date_Creation = DateTime.Now;
        }
    }
    
}
