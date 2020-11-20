﻿using Newtonsoft.Json;

namespace GloballyPaid
{
    public class Address : Entity
    {
        [JsonProperty("line_1")]
        public string Line1 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }
    }
}
