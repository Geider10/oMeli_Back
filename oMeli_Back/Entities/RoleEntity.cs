namespace oMeli_Back.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
