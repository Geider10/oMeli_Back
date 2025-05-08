using oMeli_Back.Utils;
using System.Text.Json.Serialization;

namespace oMeli_Back.DTOs.Subscription
{
    public class GetStoreByUserDto
    {
        public string SubscriptionId { get; set; }
        public string NamePlan { get; set; }
        public string State { get; set; }
        public bool Renovation { get; set; }
        [JsonConverter(typeof(DateConverter))]
        public DateTime DateStart { get; set; }
        [JsonConverter(typeof(DateConverter))]
        public DateTime DateEnd { get; set; }
    }
}
