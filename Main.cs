using Bannerlord.UIExtenderEx;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace BetterTime
{
    // This mod adds an extra speed button to the time control panel. It is a fork of the original mod by Shemiroth, which I took over after it was discontinued.
    public class Main : MBSubModuleBase
    {
        private float _currentSpeed;
        private CampaignTimeControlMode _currentTimeMode;

        protected override void OnSubModuleLoad()
        {
            UIExtender uiExtender = UIExtender.Create("BetterTime");

            uiExtender.Register(typeof(Main).Assembly);
            uiExtender.Enable();
        }

        protected override void OnApplicationTick(float dt)
        {
            Campaign campaign = Campaign.Current;

            if (campaign != null)
            {
                IInputContext input = MapScreen.Instance?.Input;

                if (input != null && (campaign.CurrentMenuContext == null || (campaign.CurrentMenuContext.GameMenu.IsWaitActive && !campaign.TimeControlModeLock)))
                {
                    Settings settings = Settings.Instance;

                    if (input.IsKeyPressed(InputKey.D3))
                    {
                        campaign.SpeedUpMultiplier = settings.FastForwardMultiplier;
                    }

                    if (input.IsKeyPressed(InputKey.D4))
                    {
                        campaign.SpeedUpMultiplier = settings.ExtraFastForwardMultiplier;
                        campaign.SetTimeSpeed(2);
                    }

                    if (input.IsControlDown() && input.IsKeyPressed(InputKey.Space))
                    {
                        campaign.SpeedUpMultiplier = settings.CtrlSpaceMultiplier;
                        campaign.SetTimeSpeed(2);
                    }

                    if ((!input.IsControlDown() || input.IsKeyReleased(InputKey.Space)) && campaign.SpeedUpMultiplier == settings.CtrlSpaceMultiplier)
                    {
                        campaign.SpeedUpMultiplier = _currentSpeed;
                        campaign.TimeControlMode = _currentTimeMode;
                    }

                    if (!input.IsControlDown() && !input.IsKeyDown(InputKey.Space))
                    {
                        _currentSpeed = campaign.SpeedUpMultiplier;
                        _currentTimeMode = campaign.TimeControlMode;
                    }
                }
            }
        }
    }
}
