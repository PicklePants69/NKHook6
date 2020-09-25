using Assets.Scripts.Simulation;

namespace NKHook6.Api.Events._Simulation
{
    public class RoundStartEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Simulation round;
            public int roundArrayIndex;

            public Pre(ref Simulation __instance, ref int roundArrayIndex) : base("Simulation.RoundStartEvent.Pre")
            {
                this.round = __instance;
                this.roundArrayIndex = roundArrayIndex;
            }
        }

        public class Post : EventBase
        {
            public Simulation round;
            public int roundArrayIndex;

            public Post(ref Simulation __instance, ref int roundArrayIndex) : base("Simulation.RoundStartEvent.Post")
            {
                this.round = __instance;
                this.roundArrayIndex = roundArrayIndex;
            }
        }
    }
}
