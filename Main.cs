using Bannerlord.ButterLib.HotKeys;
using Bannerlord.UIExtenderEx;
using HarmonyLib;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ScreenSystem;

namespace BetterTime
{
    // This mod adds an extra speed button to the time control panel. It is a fork of the original mod by Shemiroth, which I took over after it was discontinued.
    public partial class Main : MBSubModuleBase
    {
        private bool _isHotKeyManagerCreated;
        private Speed _currentSpeed;
        private CampaignTimeControlMode _currentTimeMode;

        protected override void OnSubModuleLoad()
        {
            UIExtender uiExtender = new UIExtender("BetterTime");

            uiExtender.Register(typeof(Main).Assembly);
            uiExtender.Enable();
            new Harmony("mod.bannerlord.bettertime").PatchAll();
        }

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
                bool isCtrlDown = false, isSpaceDown = false;

                d3.Predicate = () => ScreenManager.TopScreen is MapScreen;
                d4.Predicate = () => ScreenManager.TopScreen is MapScreen;
                space.Predicate = () => ScreenManager.TopScreen is MapScreen;
                d3.OnPressedEvent += () => OnPressed(d3);
                d4.OnPressedEvent += () => OnPressed(d4);
                lCtrl.OnPressedEvent += () => isCtrlDown = true;

                lCtrl.OnReleasedEvent += () =>
                {
                    isCtrlDown = false;
                    OnReleased(lCtrl);
                };

                rCtrl.OnPressedEvent += () => isCtrlDown = true;

                rCtrl.OnReleasedEvent += () =>
                {
                    isCtrlDown = false;
                    OnReleased(rCtrl);
                };

                space.OnPressedEvent += () =>
                {
                    if (isCtrlDown)
                    {
                        isSpaceDown = true;
                        OnPressed(space);
                    }
                };

                space.IsDownEvent += () =>
                {
                    if (isSpaceDown)
                    {
                        IsDown(space);
                    }
                };

                space.OnReleasedEvent += () =>
                {
                    isSpaceDown = false;
                    OnReleased(space);
                };

                hotKeyManager.Build();

                _isHotKeyManagerCreated = true;
            }
        }

        private void OnPressed(HotKeyBase hotKey)
        {
            if (!PlayerEncounter.IsActive || (PlayerEncounter.IsActive && PlayerEncounter.Current.IsPlayerWaiting) || (PlayerEncounter.InsideSettlement && PlayerEncounter.EncounterSettlement.IsHideout))
            {
                if (hotKey is HotKeys.D3)
                {
                    Support.SetTimeSpeed(Speed.FastForward);
                }
                else if (hotKey is HotKeys.D4)
                {
                    Support.SetTimeSpeed(Speed.ExtraFastForward);
                    Campaign.Current.SetTimeSpeed(2);
                }
                else if (hotKey is HotKeys.Space)
                {
                    _currentSpeed = Support.TimeSpeed;
                    _currentTimeMode = Campaign.Current.TimeControlMode;
                }
            }
        }

        private void IsDown(HotKeyBase hotKey)
        {
            if (!PlayerEncounter.IsActive || (PlayerEncounter.IsActive && PlayerEncounter.Current.IsPlayerWaiting) || (PlayerEncounter.InsideSettlement && PlayerEncounter.EncounterSettlement.IsHideout))
            {
                if (hotKey is HotKeys.Space)
                {
                    Support.SetTimeSpeed(Speed.CtrlSpace);
                    Campaign.Current.SetTimeSpeed(2);
                }
            }
        }

        private void OnReleased(HotKeyBase hotKey)
        {
            if (hotKey is HotKeys.LCtrl || hotKey is HotKeys.RCtrl || hotKey is HotKeys.Space)
            {
                if (Support.TimeSpeed == Speed.CtrlSpace)
                {
                    Support.SetTimeSpeed(_currentSpeed);
                    Campaign.Current.TimeControlMode = _currentTimeMode;
                }
            }
        }
    }
}
