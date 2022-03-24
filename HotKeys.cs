using Bannerlord.ButterLib.HotKeys;
using TaleWorlds.InputSystem;

namespace BetterTime
{
    public class HotKeys
    {
        public class D3 : HotKeyBase
        {
            protected override InputKey DefaultKey { get; }

            public D3() : base(nameof(D3)) => DefaultKey = InputKey.D3;
        }

        public class D4 : HotKeyBase
        {
            protected override InputKey DefaultKey { get; }

            public D4() : base(nameof(D4)) => DefaultKey = InputKey.D4;
        }

        public class LCtrl : HotKeyBase
        {
            protected override InputKey DefaultKey { get; }

            public LCtrl() : base(nameof(LCtrl)) => DefaultKey = InputKey.LeftControl;
        }

        public class RCtrl : HotKeyBase
        {
            protected override InputKey DefaultKey { get; }

            public RCtrl() : base(nameof(RCtrl)) => DefaultKey = InputKey.RightControl;
        }

        public class Space : HotKeyBase
        {
            protected override InputKey DefaultKey { get; }

            public Space() : base(nameof(Space)) => DefaultKey = InputKey.Space;
        }
    }
}
