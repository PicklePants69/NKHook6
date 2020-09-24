using Assets.Scripts.Simulation;

namespace NKHook6.Api.Events.Simulate
{
    public class RoundStartEvent
    {
        public class Prefix : EventBase
        {
            public Simulation round;
            public int roundArrayIndex;
            public bool replaceMethod { get; set; }

            public Prefix(ref Simulation __instance, ref int roundArrayIndex) : base("RoundStartEvent.Pre")
            {
                this.round = __instance;
                this.roundArrayIndex = roundArrayIndex;
            }
        }

        public class Postfix : EventBase
        {
            public Simulation round;
            public int roundArrayIndex;

            public Postfix(ref Simulation __instance, ref int roundArrayIndex) : base("RoundStartEvent.Post")
            {
                this.round = __instance;
                this.roundArrayIndex = roundArrayIndex;
            }
        }
    }
}
