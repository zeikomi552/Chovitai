using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Common.Enums
{
    public enum SamplerIndexEnum   
    {
        [Description("DPM++ 2M Karras")]
        DPMpp_2M_Karras,
        [Description("DPM++ SDE Karras")]
        DPMpp_SDE_Karras,
        [Description("DPM++ 2M SDE Exponential")]
        DPMpp_2M_SDE_Exponentia,
        [Description("DPM++ 2M SDE Karras")]
        DPMpp_2M_SDE_Karras,
        [Description("Euler a")]
        Euler_a,
        [Description("Euler")]
        Euler,
        [Description("LMS")]
        LMS,
        [Description("Heun")]
        Heun,
        [Description("DPM2")]
        DPM2,
        [Description("DPM2 a")]
        DPM2_a,
        [Description("DPM++ 2S a")]
        DPMpp_2S_a,
        [Description("DPM++ 2M")]
        DPMpp_2M,
        [Description("DPM++ SDE")]
        DPMpp_SDE,
        [Description("DPM++ 2M SDE")]
        DPMpp_2M_SDE,
        [Description("DPM++ 2M SDE Heun")]
        DPMpp_2M_SDE_Heun,
        [Description("DPM++ 2M SDE Heun Karras")]
        DPMpp_2M_SDE_Heun_Karras,
        [Description("DPM++ 2M SDE Heun Exponential")]
        DPMpp_2M_SDE_Heun_Exponential,
        [Description("DPM++ 3M SDE")]
        DPMpp_3M_SDE,
        [Description("DPM++ 3M SDE Karras")]
        DPMpp_3M_SDE_Karras,
        [Description("DPM++ 3M SDE Exponential")]
        DPMpp_3M_SDE_Exponential,
        [Description("DPM fast")]
        DPM_fast,
        [Description("DPM adaptive")]
        DPM_adaptive,
        [Description("LMS Karras")]
        LMS_Karras,
        [Description("DPM2 Karras")]
        DPM2_Karras,
        [Description("DPM2 a Karras")]
        DPM2_a_Karras,
        [Description("DPM++ 2S a Karras")]
        DPMpp_2S_a_Karras,
        [Description("Restart")]
        Restart,
        [Description("DDIM")]
        DDIM,
        [Description("PLMS")]
        PLMS,
        [Description("UniPC")]
        UniPC,
    }
}
