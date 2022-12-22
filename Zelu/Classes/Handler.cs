using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelu.Classes
{
    class Handler
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("methods")] public string Methods { get; set; }
    }
       
}
