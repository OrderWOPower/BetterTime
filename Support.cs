using TaleWorlds.CampaignSystem;

namespace BetterTime
{
    public static class Support
    {
        public static bool IsSpaceDown { get; set; }

        public static float CurrentSpeed { get; set; }

        public static CampaignTimeControlMode CurrentTimeMode { get; set; }

        public static void SetSpaceDown(bool isSpaceDown) => IsSpaceDown = isSpaceDown;

        public static void SetSpeed(float currentSpeed) => CurrentSpeed = currentSpeed;

        public static void SetTimeMode(CampaignTimeControlMode currentTimeMode) => CurrentTimeMode = currentTimeMode;
    }
}
