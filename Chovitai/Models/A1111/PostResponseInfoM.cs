using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chovitai.Models.A1111
{
    public class PostResponseInfoM : ModelBase
    {
        //public class InfoM
        //{

        //}

        [JsonPropertyName("info")]
        public string? Info { get; set; }
    }
}
