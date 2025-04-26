using oMeli_Back.Entities;

namespace oMeli_Back.Entities
{
    public class FollowerEntity
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateCreation { get; set; }

        public StoreEntity Store { get; set; }
        public UserEntity User { get; set; }

        public FollowerEntity()
        {
            Id = Guid.NewGuid();
            DateCreation = DateTime.Now;
        }
    }
}
