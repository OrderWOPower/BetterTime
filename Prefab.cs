using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using System.Collections.Generic;

namespace BetterTime
{
    public class Prefab
    {
        // Replace the Center Panel with a stretched version to fit the Extra Fast Forward button.
        [PrefabExtension("MapBar", "descendant::MapCurrentTimeVisualWidget", "MapBar")]
        public class PrefabCenterPanel : PrefabExtensionInsertPatch
        {
            public override InsertType Type => InsertType.ReplaceKeepChildren;

            [PrefabExtensionText(true)]
            public string Text => "<DiscardedRoot><TimePanel DataSource=\"{MapTimeControl}\" Id=\"CenterPanel\" VisualDefinition=\"CenterPanel\" WidthSizePolicy=\"Fixed\" HeightSizePolicy=\"Fixed\" SuggestedWidth=\"500\" SuggestedHeight=\"59\" HorizontalAlignment=\"Center\" VerticalAlignment=\"Bottom\" Sprite=\"MapBar\\mapbar_center_frame\" CurrentTimeState=\"@TimeFlowState\" FastFastForwardButton=\"FastFastForwardButton\" FastForwardButton=\"FastForwardButton\" IsEnabled=\"@IsCenterPanelEnabled\" PauseButton=\"PauseButton\" PlayButton=\"PlayButton\"></TimePanel></DiscardedRoot>";
        }

        // Reposition the date due to the stretched Center Panel.
        [PrefabExtension("MapBar", "descendant::TimePanel/Children/TextWidget", "MapBar")]
        public class PrefabDate : PrefabExtensionSetAttributePatch
        {
            public override List<Attribute> Attributes => new List<Attribute> { new Attribute("SuggestedWidth", "220"), new Attribute("PositionXOffset", "15") };
        }

        // Add the Extra Fast Forward button to the Center Panel.
        [PrefabExtension("MapBar", "descendant::TimePanel/Children/ButtonWidget[@Id='FastForwardButton']", "MapBar")]
        public class PrefabFastFastForwardButton : PrefabExtensionInsertPatch
        {
            public override InsertType Type => InsertType.Prepend;

            [PrefabExtensionText]
            public string Text => "<ButtonWidget Id=\"FastFastForwardButton\" WidthSizePolicy=\"Fixed\" HeightSizePolicy=\"Fixed\" SuggestedWidth=\"35\" SuggestedHeight=\"24\" HorizontalAlignment=\"Right\" VerticalAlignment=\"Bottom\" PositionXOffset=\"-62\" PositionYOffset=\"-13\" Brush=\"MapBarFastForwardButton\" Command.Click=\"ExecuteTimeControlChange\" CommandParameter.Click=\"2\"><Children><HintWidget DataSource=\"{FastFastForwardHint}\" WidthSizePolicy=\"StretchToParent\" HeightSizePolicy=\"StretchToParent\" Command.HoverBegin=\"ExecuteBeginHint\" Command.HoverEnd=\"ExecuteEndHint\" IsDisabled=\"true\" /></Children></ButtonWidget>";
        }

        // Reposition the Fast Forward button due to the stretched Center Panel.
        [PrefabExtension("MapBar", "descendant::TimePanel/Children/ButtonWidget[@Id='FastForwardButton']", "MapBar")]
        public class PrefabFastForwardButton : PrefabExtensionSetAttributePatch
        {
            public override List<Attribute> Attributes => new List<Attribute> { new Attribute("PositionXOffset", "-105") };
        }

        // Reposition the Play button due to the stretched Center Panel.
        [PrefabExtension("MapBar", "descendant::TimePanel/Children/ButtonWidget[@Id='PlayButton']", "MapBar")]
        public class PrefabPlayButton : PrefabExtensionSetAttributePatch
        {
            public override List<Attribute> Attributes => new List<Attribute> { new Attribute("PositionXOffset", "-145") };
        }

        // Reposition the Pause button due to the stretched Center Panel.
        [PrefabExtension("MapBar", "descendant::TimePanel/Children/ButtonWidget[@Id='PauseButton']", "MapBar")]
        public class PrefabPauseButton : PrefabExtensionSetAttributePatch
        {
            public override List<Attribute> Attributes => new List<Attribute> { new Attribute("PositionXOffset", "-185") };
        }
    }
}
