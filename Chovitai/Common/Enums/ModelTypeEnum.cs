using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Common.Enums
{
    public enum ModelTypeEnum
    {
        [Description("")]
        Empty,
        [Description("Checkpoint")]
        Checkpoint,
        [Description("TextualInversion")]
        TextualInversion,
        [Description("Hypernetwork")]
        Hypernetwork,
        [Description("AestheticGradient")]
        AestheticGradient,
        [Description("LORA")]
        LORA,
        [Description("LoCon")]
        LoCon,
        [Description("Controlnet")]
        Controlnet,
        [Description("Upscaler")]
        Upscaler,
        [Description("MotionModule")]
        MotionModule,
        [Description("VAE")]
        VAE,
        [Description("Poses")]
        Poses,
        [Description("Wildcards")]
        Wildcards,
        [Description("Workflows")]
        Workflows,
        [Description("Other")]
        Other
    }
}
