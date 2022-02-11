using Bannerlord.ButterLib.HotKeys;
using Bannerlord.UIExtenderEx;
using HarmonyLib;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine.Screens;
using TaleWorlds.MountAndBlade;

namespace BetterTime
{
    // This mod adds an extra speed button to the time control panel. It is a fork of the original mod by Shemiroth, which I took over after it was discontinued.
    public partial class Main : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            new Harmony("mod.bannerlord.bettertime").PatchAll();
            UIExtender uiExtender = new UIExtender("BetterTime");
            uiExtender.Register(typeof(Main).Assembly);
            uiExtender.Enable();
        }

        // Set the speed multipliers when the respective keys are pressed.
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            if (!_isHotKeyManagerCreated)
            {
                HotKeyManager hotKeyManager = HotKeyManager.Create("BetterTime");
                HotKeys.D3 d3 = hotKeyManager.Add<HotKeys.D3>();
                HotKeys.D4 d4 = hotKeyManager.Add<HotKeys.D4>();
                HotKeys.LCtrl lCtrl = hotKeyManager.Add<HotKeys.LCtrl>();
                HotKeys.RCtrl rCtrl = hotKeyManager.Add<HotKeys.RCtrl>();
                HotKeys.Space space = hotKeyManager.Add<HotKeys.Space>();
                d3.Predicate = () => ScreenManager.TopScreen is MapScreen;
                d4.Predicate = () => ScreenManager.TopScreen is MapScreen;
                space.Predicate = () => ScreenManager.TopScreen is MapScreen;
                bool isCtrlDown = false;
                d3.OnPressedEvent += () => Support.SetSpeed(true, false);
                d4.OnPressedEvent += () =>
                {
                    Support.SetSpeed(false, true);
                    Campaign.Current.SetTimeSpeed(2);
                };
                lCtrl.OnPressedEvent += () => isCtrlDown = true;
                lCtrl.OnReleasedEvent += () =>
                {
                    isCtrlDown = false;
                    if (Support.IsSpeedCtrlSpace)
                    {
                        Support.SetSpeed(false);
                        Campaign.Current.TimeControlMode = _currentTimeMode;
                    }
                };
                rCtrl.OnPressedEvent += () => isCtrlDown = true;
                rCtrl.OnReleasedEvent += () =>
                {
                    isCtrlDown = false;
                    if (Support.IsSpeedCtrlSpace)
                    {
                        Support.SetSpeed(false);
                        Campaign.Current.TimeControlMode = _currentTimeMode;
                    }
                };
                space.OnPressedEvent += () =>
                {
                    if (isCtrlDown)
                    {
                        Support.SetSpeed(true);
                        _currentTimeMode = Campaign.Current.TimeControlMode;
                    }
                };
                space.IsDownEvent += () =>
                {
                    if (Support.IsSpeedCtrlSpace)
                    {
                        Campaign.Current.SetTimeSpeed(2);
                    }
                };
                space.OnReleasedEvent += () =>
                {
                    if (Support.IsSpeedCtrlSpace)
                    {
                        Support.SetSpeed(false);
                        Campaign.Current.TimeControlMode = _currentTimeMode;
                    }
                };
                hotKeyManager.Build();
                _isHotKeyManagerCreated = true;
            }
        }

        private bool _isHotKeyManagerCreated;
        private CampaignTimeControlMode _currentTimeMode;
    }
}
