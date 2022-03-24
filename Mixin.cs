using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;

namespace BetterTime
{
    [ViewModelMixin]
    public class Mixin : BaseViewModelMixin<MapTimeControlVM>
    {
        private BasicTooltipViewModel _fastFastForwardHint;

        [DataSourceProperty]
        public BasicTooltipViewModel FastFastForwardHint
        {
            get => _fastFastForwardHint;
            set
            {
                if (value != _fastFastForwardHint)
                {
                    _fastFastForwardHint = value;
                }
            }
        }

        // Add the hint tooltip to the Extra Fast Forward button.
        public Mixin(MapTimeControlVM mapTimeControlVM) : base(mapTimeControlVM)
        {
            FastFastForwardHint = new BasicTooltipViewModel(delegate ()
            {
                GameTexts.SetVariable("TEXT", "Extra Fast Forward");
                GameTexts.SetVariable("HOTKEY", "4");
                return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
            });
        }
    }
}
