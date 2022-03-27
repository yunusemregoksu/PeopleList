using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleList.Model
{
    public class OData<T>
    {
        [JsonProperty("odata.context")]
        public string Metadata { get; set; }
        [JsonProperty("value")]
        public T Value { get; set; }
    }
}
