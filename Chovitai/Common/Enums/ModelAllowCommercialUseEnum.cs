using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Common.Enums
{
    public enum ModelAllowCommercialUseEnum
    {
        [Description("")]
        Empty,
        [Description("None")]
        None,
        [Description("Image")]
        Image,
        [Description("Rent")]
        Rent,
        [Description("Sell")]
        Sell
    }
}
