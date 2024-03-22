using Newtonsoft.Json;

namespace CalendarConsole.Models;

public class JsonMail
{
    public class ResourceData
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }

        [JsonProperty("@odata.id")]
        public string odataid { get; set; }

        [JsonProperty("@odata.etag")]
        public string odataetag { get; set; }
        public string id { get; set; }
    }

    public class Root
    {
        public List<Value> value { get; set; }
    }

    public class Value
    {
        public string subscriptionId { get; set; }
        public DateTime subscriptionExpirationDateTime { get; set; }
        public string changeType { get; set; }
        public string resource { get; set; }
        public ResourceData resourceData { get; set; }
        public object clientState { get; set; }
        public string tenantId { get; set; }
    }
}