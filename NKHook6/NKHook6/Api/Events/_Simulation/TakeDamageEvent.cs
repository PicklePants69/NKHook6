using Assets.Scripts.Simulation;
using System;
using System.Collections.Generic;
namespace NKHook6.Api.Events._Simulation
{
	public class TakeDamageEvent
	{
		public class Pre : EventBaseCancellable
		{
			public Simulation simulation;
			public float damage;

			public Pre(ref Simulation __instance, ref float damage) : base("Simulation.TakeDamageEvent.Pre")
			{
				this.simulation = __instance;
				this.damage = damage;
			}
		}

		public class Post : EventBase
		{
			public Simulation simulation;
			public float damage;

			public Post(ref Simulation __instance, ref float damage) : base("Simulation.TakeDamageEvent.Post")
			{
				this.simulation = __instance;
				this.damage = damage;
			}
		}
	}

}
