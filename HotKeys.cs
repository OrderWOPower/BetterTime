using Bannerlord.ButterLib.HotKeys;
using TaleWorlds.InputSystem;

namespace BetterTime
{
    public class HotKeys
    {
        public class D3 : HotKeyBase
        {
            public D3() : base(nameof(D3)) => DefaultKey = InputKey.D3;
            protected override InputKey DefaultKey { get; }
        }
        public class D4 : HotKeyBase
        {
            public D4() : base(nameof(D4)) => DefaultKey = InputKey.D4;
            protected override InputKey DefaultKey { get; }
        }
        public class LCtrl : HotKeyBase
        {
            public LCtrl() : base(nameof(LCtrl)) => DefaultKey = InputKey.LeftControl;
            protected override InputKey DefaultKey { get; }
        }
        public class RCtrl : HotKeyBase
        {
            public RCtrl() : base(nameof(RCtrl)) => DefaultKey = InputKey.RightControl;
            protected override InputKey DefaultKey { get; }
        }
        public class Space : HotKeyBase
        {
            public Space() : base(nameof(Space)) => DefaultKey = InputKey.Space;
            protected override InputKey DefaultKey { get; }
        }
    }
}
