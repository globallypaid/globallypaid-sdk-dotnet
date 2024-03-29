﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GloballyPaid
{
    public class PaymentInstrumentRequest : Request
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentType Type { get; set; }

        [JsonProperty("credit_card")]
        public CreditCard CreditCard { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("client_customer_id")]
        public string ClientCustomerId { get; set; }

        [JsonProperty("billing_contact")]
        public Contact BillingContact { get; set; }
    }
}
