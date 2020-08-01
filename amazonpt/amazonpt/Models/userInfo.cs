using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace amazonpt.Models
{
    [JsonObject]
    public class userInfo
    {
        [JsonProperty("playerId")]
        public string PlayerId { get; set; }

        [JsonProperty("pushToken")]
        public string PushToken { get; set; }
    }
}
