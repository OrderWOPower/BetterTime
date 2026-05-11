using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace BetterTime
{
	[ViewModelMixin("RefreshValues")]
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

		public Mixin(MapTimeControlVM mapTimeControlVM) : base(mapTimeControlVM) => FastFastForwardHint = new BasicTooltipViewModel();

		public override void OnRefresh()
		{
			if (!Input.IsGamepadActive)
			{
				FastFastForwardHint.SetHintCallback(delegate
				{
					// Add the hint tooltip to the Extra Fast Forward button.
					GameTexts.SetVariable("TEXT", new TextObject("{=BT006.1693}Extra Fast Forward"));
					GameTexts.SetVariable("HOTKEY", "4");

					return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
				});
			}
		}
	}
}
