namespace oMeli_Back.Entities
{
    public class ImageEntity
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }//save id of store or product
        public string NameEntity { get; set; }
        public string DetailImg { get; set; }
        public string UrlImg { get; set; }
        public DateTime DateCreation { get; set; }

        public StoreEntity Store { get; set; }

        public ImageEntity()
        {
            Id = Guid.NewGuid();
            DateCreation = DateTime.Now;
        }
    }
}
