using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Common.Enums
{
    public enum ModelPeriodEnum
    {
        [Description("")]
        Empty,
        [Description("AllTime")]
        AllTime,
        [Description("Year")]
        Year,
        [Description("Month")]
        Month,
        [Description("Week")]
        Week,
        [Description("Day")]
        Day
    }
}
