using Assets.Scripts.Simulation;
using System;
using System.Collections.Generic;
namespace NKHook6.Api.Events._Simulation
{
	public class SimulationTakeDamageEvent
	{
		public class Prefix : EventBaseCancellable
		{
			public Simulation simulation;
			public float damage;

			public Prefix(ref Simulation __instance, ref float damage) : base("SimulationTakeDamageEvent.Pre")
			{
				this.simulation = __instance;
				this.damage = damage;
			}
		}

		public class Postfix : EventBase
		{
			public Simulation simulation;
			public float damage;

			public Postfix(ref Simulation __instance, ref float damage) : base("SimulationTakeDamageEvent.Post")
			{
				this.simulation = __instance;
				this.damage = damage;
			}
		}
	}

}
