using TaleWorlds.GauntletUI;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map;

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

        // Set the speeds when the respective buttons are clicked.
        protected override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);
            if (IsDisabled)
            {
                SetState("Disabled");
            }
            else
            {
                var fffc = FastFastForwardButton.ClickEventHandlers;
                var ffc = FastForwardButton.ClickEventHandlers;
                if (fffc.Count == 0)
                {
                    fffc.Add(a => Support.SetTimeSpeed(Speed.ExtraFastForward));
                }
                if (ffc.Count == 0)
                {
                    ffc.Add(a => Support.SetTimeSpeed(Speed.FastForward));
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
                        if (Support.TimeSpeed == Speed.ExtraFastForward || Support.TimeSpeed == Speed.CtrlSpace)
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
        }
    }
}
