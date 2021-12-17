using HarmonyLib;
using TaleWorlds.CampaignSystem;

namespace BetterTime
{
    [HarmonyPatch(typeof(Campaign), "TickMapTime")]
    public class Patches
    {
        private static void Prefix(ref float realDt)
        {
            Settings settings = Settings.Instance;
            if (!Support.IsSpeedCtrlSpace)
            {
                if (Support.IsSpeedFastForward)
                {
                    realDt *= settings.FastForwardMultiplier / 4;
                }
                else if (Support.IsSpeedExtraFastForward)
                {
                    realDt *= settings.ExtraFastForwardMultiplier / 4;
                }
            }
            else
            {
                realDt *= settings.CtrlSpaceMultiplier / 4;
            }
        }
    }
}
