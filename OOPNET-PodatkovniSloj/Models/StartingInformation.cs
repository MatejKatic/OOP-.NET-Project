using Newtonsoft.Json;
using OOPNET_ProjektniZadatak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPNET_PodatkovniSloj.Models
{
    public partial class StartingInformation
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("captain", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Captain { get; set; }

        [JsonProperty("shirt_number", NullValueHandling = NullValueHandling.Ignore)]
        public long? ShirtNumber { get; set; }

        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public Position? Position { get; set; }
    }
}
