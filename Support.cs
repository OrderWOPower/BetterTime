using HarmonyLib;
using TaleWorlds.CampaignSystem;

namespace BetterTime
{
    [HarmonyPatch(typeof(Campaign), "TickMapTime")]
    public static class Support
    {
        private static Speed _speed;

        public static Speed TimeSpeed
        {
            get => _speed;
            set
            {
                if (value != _speed)
                {
                    _speed = value;
                }
            }
        }

        private static void Prefix(ref float realDt)
        {
            Settings settings = Settings.Instance;
            if (TimeSpeed == Speed.FastForward)
            {
                realDt *= settings.FastForwardMultiplier / 4f;
            }
            else if (TimeSpeed == Speed.ExtraFastForward)
            {
                realDt *= settings.ExtraFastForwardMultiplier / 4f;
            }
            else if (TimeSpeed == Speed.CtrlSpace)
            {
                realDt *= settings.CtrlSpaceMultiplier / 4f;
            }
        }

        public static void SetTimeSpeed(Speed speed) => TimeSpeed = speed;
    }
}
