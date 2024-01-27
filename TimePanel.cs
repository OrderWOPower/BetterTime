using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.MapBar;

namespace BetterTime
{
    public class TimePanel : MapCurrentTimeVisualWidget
    {
        private ButtonWidget _fastFastForwardButton;

        [Editor(false)]
        public ButtonWidget FastFastForwardButton
        {
            get
            {
                return _fastFastForwardButton;
            }

            set
            {
                if (_fastFastForwardButton == value)
                    return;

                _fastFastForwardButton = value;

                OnPropertyChanged(value, nameof(FastFastForwardButton));
            }
        }

        public TimePanel(UIContext context)
            : base(context)
        {
            AddState("Disabled");
        }

        protected override void OnUpdate(float dt)
        {
            Campaign campaign = Campaign.Current;
            Settings settings = Settings.Instance;

            base.OnUpdate(dt);

            if (IsDisabled)
            {
                SetState("Disabled");
            }
            else
            {
                var ffc = FastForwardButton.ClickEventHandlers;
                var fffc = FastFastForwardButton.ClickEventHandlers;

                if (!ffc.Any())
                {
                    ffc.Add(a => campaign.SpeedUpMultiplier = settings.FastForwardMultiplier);
                }

                if (!fffc.Any())
                {
                    fffc.Add(a => campaign.SpeedUpMultiplier = settings.ExtraFastForwardMultiplier);
                }

                SetState("Default");
                PauseButton.IsSelected = false;
                PlayButton.IsSelected = false;
                FastForwardButton.IsSelected = false;
                FastFastForwardButton.IsSelected = false;

                switch (CurrentTimeState)
                {
                    case 0:
                    case 6:
                        PauseButton.IsSelected = true;

                        break;
                    case 1:
                    case 3:
                        PlayButton.IsSelected = true;

                        break;
                    case 2:
                    case 4:
                    case 5:
                        if (campaign.SpeedUpMultiplier > settings.FastForwardMultiplier)
                        {
                            FastFastForwardButton.IsSelected = true;
                        }
                        else
                        {
                            FastForwardButton.IsSelected = true;
                        }

                        break;
                }
            }

            if (!Support.IsSpaceDown && campaign.SpeedUpMultiplier == settings.CtrlSpaceMultiplier)
            {
                campaign.SpeedUpMultiplier = Support.CurrentSpeed;

                if (campaign.CurrentMenuContext == null || (campaign.CurrentMenuContext.GameMenu.IsWaitActive && !campaign.TimeControlModeLock))
                {
                    campaign.TimeControlMode = Support.CurrentTimeMode;
                }
            }

            Support.SetSpaceDown(false);
        }
    }
}
