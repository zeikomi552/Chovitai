using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Common.Enums
{
    public enum ImageNsfwEnum
    {
        [Description("")]
        Empty,
        [Description("None")]
        None,
        [Description("Soft")]
        Soft,
        [Description("Mature")]
        Mature,
        [Description("X")]
        X
    }
    
}
