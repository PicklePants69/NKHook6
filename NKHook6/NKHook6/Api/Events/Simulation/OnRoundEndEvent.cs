using Assets.Scripts.Simulation;

namespace NKHook6.Api.Events._Simulation
{
    public partial class SimulationEvents
    {
        public class OnRoundEndEvent
        {
            public class Pre : EventBaseCancellable
            {
                public Simulation instance;
                public int roundArrayIndex;

                public Pre(ref Simulation __instance, ref int round) : base("Simulation.OnRoundEndEvent.Pre")
                {
                    this.instance = __instance;
                    this.roundArrayIndex = round;
                }
            }

            public class Post : EventBase
            {
                public Simulation instance;
                public int roundArrayIndex;

                public Post(ref Simulation __instance, ref int round) : base("Simulation.OnRoundEndEvent.Post")
                {
                    this.instance = __instance;
                    this.roundArrayIndex = round;
                }
            }
        }
    }
}
