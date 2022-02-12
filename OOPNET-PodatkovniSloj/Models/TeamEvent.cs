using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPNET_PodatkovniSloj.Models
{
    public partial class TeamEvent
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("type_of_event", NullValueHandling = NullValueHandling.Ignore)]
        public string TypeOfEvent { get; set; }

        [JsonProperty("player", NullValueHandling = NullValueHandling.Ignore)]
        public string Player { get; set; }

        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public string Time { get; set; }
    }
}
