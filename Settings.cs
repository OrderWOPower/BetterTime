using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Settings.Base.Global;

namespace BetterTime
{
    public class Settings : AttributeGlobalSettings<Settings>
    {
        // The default settings for the Fast Forward button, Extra Fast Forward button and Ctrl + Space are 1x, 2x and 3x the base speed multiplier (4x). The actual speed multipliers are 4x, 8x and 12x.
        public override string Id => "BetterTime";
        public override string DisplayName => "Better Time";
        public override string FolderName => "BetterTime";
        public override string FormatType => "json2";
        [SettingPropertyInteger("Fast Forward Button", 1, 8, "0x", Order = 0, RequireRestart = false, HintText = "Speed multiplier for the fast forward button. Default is 1x.")]
        [SettingPropertyGroup("Speed Multipliers")]
        public int FastForwardMultiplier { get; set; } = 1;
        [SettingPropertyInteger("Extra Fast Forward Button", 1, 8, "0x", Order = 1, RequireRestart = false, HintText = "Speed multiplier for the extra fast forward button. Default is 2x.")]
        [SettingPropertyGroup("Speed Multipliers")]
        public int ExtraFastForwardMultiplier { get; set; } = 2;
        [SettingPropertyInteger("Ctrl + Space", 1, 8, "0x", Order = 2, RequireRestart = false, HintText = "Speed multiplier for holding ctrl + space. Default is 3x.")]
        [SettingPropertyGroup("Speed Multipliers")]
        public int CtrlSpaceMultiplier { get; set; } = 3;
    }
}
