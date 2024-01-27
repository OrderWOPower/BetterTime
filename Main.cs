using Bannerlord.ButterLib.HotKeys;
using Bannerlord.UIExtenderEx;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ScreenSystem;
using static BetterTime.HotKeys;

namespace BetterTime
{
    // This mod adds an extra speed button to the time control panel. It is a fork of the original mod by Shemiroth, which I took over after it was discontinued.
    public partial class Main : MBSubModuleBase
    {
        private bool _isHotKeyManagerCreated;

        protected override void OnSubModuleLoad()
        {
            UIExtender uiExtender = UIExtender.Create("BetterTime");

            uiExtender.Register(typeof(Main).Assembly);
            uiExtender.Enable();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            if (!_isHotKeyManagerCreated)
            {
                HotKeyManager hotKeyManager = HotKeyManager.Create("BetterTime");
                LCtrl lCtrl = hotKeyManager.Add<LCtrl>();
                RCtrl rCtrl = hotKeyManager.Add<RCtrl>();
                D3 d3 = hotKeyManager.Add<D3>();
                D4 d4 = hotKeyManager.Add<D4>();
                Space space = hotKeyManager.Add<Space>();
                Settings settings = Settings.Instance;

                lCtrl.Predicate = () => Campaign.Current != null;
                rCtrl.Predicate = () => Campaign.Current != null;
                d3.Predicate = () => Campaign.Current != null && (Campaign.Current.CurrentMenuContext == null || (Campaign.Current.CurrentMenuContext.GameMenu.IsWaitActive && !Campaign.Current.TimeControlModeLock)) && ScreenManager.TopScreen is MapScreen;
                d4.Predicate = () => Campaign.Current != null && (Campaign.Current.CurrentMenuContext == null || (Campaign.Current.CurrentMenuContext.GameMenu.IsWaitActive && !Campaign.Current.TimeControlModeLock)) && ScreenManager.TopScreen is MapScreen;
                space.Predicate = () => Campaign.Current != null && (Campaign.Current.CurrentMenuContext == null || (Campaign.Current.CurrentMenuContext.GameMenu.IsWaitActive && !Campaign.Current.TimeControlModeLock)) && ScreenManager.TopScreen is MapScreen;

                lCtrl.OnPressedEvent += () =>
                {
                    Support.SetSpeed(Campaign.Current.SpeedUpMultiplier);
                    Support.SetTimeMode(Campaign.Current.TimeControlMode);
                    space.IsEnabled = true;
                };
                lCtrl.OnReleasedEvent += () => space.IsEnabled = false;
                rCtrl.OnPressedEvent += () =>
                {
                    Support.SetSpeed(Campaign.Current.SpeedUpMultiplier);
                    Support.SetTimeMode(Campaign.Current.TimeControlMode);
                    space.IsEnabled = true;
                };
                rCtrl.OnReleasedEvent += () => space.IsEnabled = false;
                d3.OnPressedEvent += () => Campaign.Current.SpeedUpMultiplier = settings.FastForwardMultiplier;
                d4.OnPressedEvent += () =>
                {
                    Campaign.Current.SpeedUpMultiplier = settings.ExtraFastForwardMultiplier;
                    Campaign.Current.SetTimeSpeed(2);
                };
                space.IsDownEvent += () =>
                {
                    Campaign.Current.SpeedUpMultiplier = settings.CtrlSpaceMultiplier;
                    Campaign.Current.SetTimeSpeed(2);
                    Support.SetSpaceDown(true);
                };
                hotKeyManager.Build();

                _isHotKeyManagerCreated = true;
            }
        }
    }
}
