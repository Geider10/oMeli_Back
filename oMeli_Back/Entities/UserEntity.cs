namespace oMeli_Back.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; } //= Guid.NewGuid();
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime Date_Creation { get; set; } // = DateTime.Now;
        public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
    }
}
