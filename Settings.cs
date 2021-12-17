using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Settings.Base.Global;

namespace BetterTime
{
    public class Settings : AttributeGlobalSettings<Settings>
    {
        // The default settings for the Fast Forward button, Extra Fast Forward button and Ctrl + Space are 4, 8 and 16.
        public override string Id => "BetterTime";
        public override string DisplayName => "Better Time";
        public override string FolderName => "BetterTime";
        public override string FormatType => "json2";
        [SettingPropertyInteger("Fast Forward Button", 1, 128, "0", Order = 0, RequireRestart = false, HintText = "Speed multiplier for the fast forward button. Default is 4.")]
        [SettingPropertyGroup("Speed Multipliers")]
        public int FastForwardMultiplier { get; set; } = 4;
        [SettingPropertyInteger("Extra Fast Forward Button", 1, 128, "0", Order = 1, RequireRestart = false, HintText = "Speed multiplier for the extra fast forward button. Default is 8.")]
        [SettingPropertyGroup("Speed Multipliers")]
        public int ExtraFastForwardMultiplier { get; set; } = 8;
        [SettingPropertyInteger("Ctrl + Space", 1, 128, "0", Order = 2, RequireRestart = false, HintText = "Speed multiplier for holding ctrl + space. Default is 16.")]
        [SettingPropertyGroup("Speed Multipliers")]
        public int CtrlSpaceMultiplier { get; set; } = 16;
    }
}
