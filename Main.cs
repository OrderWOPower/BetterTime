using Bannerlord.UIExtenderEx;
using HarmonyLib;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
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

        private CampaignTimeControlMode _currentTimeMode;

        // Set the speed multipliers when the respective keys are pressed.
        protected override void OnApplicationTick(float dt)
        {
            if (ScreenManager.TopScreen is MapScreen)
            {
                if (Campaign.Current.CurrentMenuContext != null && (!Campaign.Current.CurrentMenuContext.GameMenu.IsWaitActive || Campaign.Current.TimeControlModeLock))
                {
                    return;
                }

                if (Input.IsKeyPressed(InputKey.D3))
                {
                    Support.SetSpeed(true, false);
                }
                if (Input.IsKeyPressed(InputKey.D4))
                {
                    Support.SetSpeed(false, true);
                    Campaign.Current.SetTimeSpeed(2);
                }

                if ((Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl)) && Input.IsKeyDown(InputKey.Space))
                {
                    if (!Support.IsSpeedCtrlSpace)
                    {
                        Support.SetSpeed(true);
                        _currentTimeMode = Campaign.Current.TimeControlMode;
                    }

                    Campaign.Current.SetTimeSpeed(2);
                }
                else
                {
                    if (Support.IsSpeedCtrlSpace)
                    {
                        Support.SetSpeed(false);
                        Campaign.Current.TimeControlMode = _currentTimeMode;
                    }
                }
            }
        }
    }
}
