using Assets.Scripts.Simulation;

namespace NKHook6.Api.Events.Simulate
{
    public class RoundEndEvent
    {
        public class Prefix : EventBase
        {
            public Simulation round;
            public int roundArrayIndex;
            public bool replaceMethod { get; set; }

            public Prefix(ref Simulation __instance, ref int round) : base("RoundEndEvent.Pre")
            {
                this.round = __instance;
                this.roundArrayIndex = round;
            }
        }

        public class Postfix : EventBase
        {
            public Simulation round;
            public int roundArrayIndex;

            public Postfix(ref Simulation __instance, ref int round) : base("RoundEndEvent.Post")
            {
                this.round = __instance;
                this.roundArrayIndex = round;
            }
        }
    }
}
