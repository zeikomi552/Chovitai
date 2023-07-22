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
        [Description("Controlnet")]
        Controlnet,
        [Description("Poses")]
        Poses,
        [Description("Wildcards")]
        Wildcards,
        [Description("VAE")]
        VAE,
        [Description("LoCon")]
        LoCon,
        [Description("Other")]
        Other
    }
}
