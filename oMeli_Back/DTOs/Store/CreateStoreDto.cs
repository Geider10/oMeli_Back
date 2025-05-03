namespace oMeli_Back.DTOs.Store
{
    public class CreateStoreDto
    {
        public string UserId { get; set; }
        public string SubscriptionId { get; set; }
        public string Name { get; set; }
        public string Wassap { get; set; }
        public string Mail { get; set; }
        public bool HasLocal { get; set; }
        public string Address { get; set; }
        public string AddressDescription { get; set; }
        public string LocalNumber { get; set; }
        public int CurrentProducts { get; set; }
    }
}
