using System.ComponentModel;
using Bannerlord.UIExtenderEx;
using MCM.Abstractions.Settings.Base;
using MCM.Abstractions.Settings.Providers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;
using SandBox.View.Map;

namespace BetterTime
{
    // This mod adds an extra speed button to the time control panel. It is a fork of the original mod by Shemiroth, which I took over after it was discontinued.
    public partial class Main : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            UIExtender uiExtender = new UIExtender("BetterTime");
            uiExtender.Register(typeof(Main).Assembly);
            uiExtender.Enable();
        }

        // If Kaoses Tweaks is loaded, set the base speed multiplier to the "Campaign Speed Fast Forward" setting. If not, set the base speed multiplier to 4.
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            _kaosesTweaksSettings = BaseSettingsProvider.Instance?.GetSettings("KaosesTweaks");
            _betterTimeSettings = Settings.Instance;
            _fastForwardSpeed = (int?)_kaosesTweaksSettings?.GetType().GetProperty("CampaignSpeed").GetValue(_kaosesTweaksSettings) ?? 4;
            if (_kaosesTweaksSettings != null)
            {
                _kaosesTweaksSettings.PropertyChanged += OnPropertyChanged;
            }
        }
        // Whenever any setting in Kaoses Tweaks is changed, set the base speed multiplier to the "Campaign Speed Fast Forward" setting.
        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => _fastForwardSpeed = (int)_kaosesTweaksSettings.GetType().GetProperty("CampaignSpeed").GetValue(_kaosesTweaksSettings);

        private BaseSettings _kaosesTweaksSettings;
        private Settings _betterTimeSettings;
        private int _fastForwardSpeed;
        private float _currentSpeed;
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
                    Support.SetSpeeds(true, false);
                }
                if (Input.IsKeyPressed(InputKey.D4))
                {
                    Support.SetSpeeds(false, true);
                    Campaign.Current.SetTimeSpeed(2);
                }

                if ((Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl)) && Input.IsKeyDown(InputKey.Space))
                {
                    if (!Support.IsSpeedCtrlSpace)
                    {
                        Support.SetSpeeds(true);
                        _currentSpeed = Campaign.Current.SpeedUpMultiplier;
                        _currentTimeMode = Campaign.Current.TimeControlMode;
                    }

                    Campaign.Current.SetTimeSpeed(2);
                }
                else
                {
                    if (Support.IsSpeedCtrlSpace)
                    {
                        Support.SetSpeeds(false);
                        Campaign.Current.SpeedUpMultiplier = _currentSpeed;
                        Campaign.Current.TimeControlMode = _currentTimeMode;
                    }
                }
                if (Support.IsSpeedFastForward)
                {
                    Campaign.Current.SpeedUpMultiplier = _fastForwardSpeed * _betterTimeSettings.FastForwardMultiplier;
                }
                else if (Support.IsSpeedExtraFastForward)
                {
                    Campaign.Current.SpeedUpMultiplier = _fastForwardSpeed * _betterTimeSettings.ExtraFastForwardMultiplier;
                }
                else
                {
                    Campaign.Current.SpeedUpMultiplier = _fastForwardSpeed;
                }
                if (Support.IsSpeedCtrlSpace)
                {
                    Campaign.Current.SpeedUpMultiplier = _fastForwardSpeed * _betterTimeSettings.CtrlSpaceMultiplier;
                }
            }
        }
    }
}
