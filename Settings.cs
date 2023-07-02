using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace BetterTime
{
    public class Settings : AttributeGlobalSettings<Settings>
    {
        public override string Id => "BetterTime";

        public override string DisplayName => "Better Time";

        public override string FolderName => "BetterTime";

        public override string FormatType => "json2";

        [SettingPropertyInteger("{=BT001.7286}Fast Forward Button", 1, 128, "0", Order = 0, RequireRestart = false, HintText = "{=BT001.7286Hint}Speed multiplier for the fast forward button. Default is 4.")]
        [SettingPropertyGroup("{=BT002.8010}Speed Multipliers")]
        public int FastForwardMultiplier { get; set; } = 4;

        [SettingPropertyInteger("{=BT003.2244}Extra Fast Forward Button", 1, 128, "0", Order = 1, RequireRestart = false, HintText = "{=BT003.2244Hint}Speed multiplier for the extra fast forward button. Default is 8.")]
        [SettingPropertyGroup("{=BT002.8010}Speed Multipliers")]
        public int ExtraFastForwardMultiplier { get; set; } = 8;

        [SettingPropertyInteger("{=BT004.1356}Ctrl + Space", 1, 128, "0", Order = 2, RequireRestart = false, HintText = "{=BT004.1356Hint}Speed multiplier for holding ctrl + space. Default is 16.")]
        [SettingPropertyGroup("{=BT002.8010}Speed Multipliers")]
        public int CtrlSpaceMultiplier { get; set; } = 16;
    }
}
