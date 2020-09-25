using Assets.Scripts.Simulation;

namespace NKHook6.Api.Events._Simulation
{
    public class RoundStartEvent
    {
        public class Prefix : EventBaseCancellable
        {
            public Simulation round;
            public int roundArrayIndex;

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
