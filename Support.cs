namespace BetterTime
{
    public static class Support
    {
        public static bool IsSpeedFastForward { get; set; }

        public static bool IsSpeedExtraFastForward { get; set; }

        public static bool IsSpeedCtrlSpace { get; set; }

        public static void SetSpeed(bool isFastForward, bool isExtraFastForward)
        {
            IsSpeedFastForward = isFastForward;
            IsSpeedExtraFastForward = isExtraFastForward;
        }

        public static void SetSpeed(bool isCtrlSpace) => IsSpeedCtrlSpace = isCtrlSpace;
    }
}
