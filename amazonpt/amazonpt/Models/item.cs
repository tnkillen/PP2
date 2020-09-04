using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace amazonpt.Models
{
    [JsonObject]
    public class item
    {
        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        [JsonProperty("desiredPrice")]
        public double DesiredPrice { get; set; }

        [JsonProperty("itemURL")]
        public string ItemURL { get; set; }

        [JsonProperty("priceAchived")]
        public bool PriceAchived { get; set; }

    }
}
