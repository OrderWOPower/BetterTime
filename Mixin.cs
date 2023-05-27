using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
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

                    ViewModel?.OnPropertyChangedWithValue(value, "FastFastForwardHint");
                }
            }
        }

        public Mixin(MapTimeControlVM mapTimeControlVM) : base(mapTimeControlVM)
        {
            FastFastForwardHint = new BasicTooltipViewModel(delegate ()
            {
                // Add the hint tooltip to the Extra Fast Forward button.
                GameTexts.SetVariable("TEXT", "Extra Fast Forward");
                GameTexts.SetVariable("HOTKEY", "4");

                return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
            });
        }
    }
}
