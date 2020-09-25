using Assets.Scripts.Simulation;
using System;
using System.Collections.Generic;
namespace NKHook6.Api.Events._Simulation
{
	public class OnDefeatEvent
	{
		public class Prefix : EventBaseCancellable
		{
			public Simulation simulation;

			public Prefix(ref Simulation __instance) : base("OnDefeatEvent.Pre")
			{
				this.simulation = __instance;
			}
		}

		public class Postfix : EventBase
		{
			public Simulation simulation;

			public Postfix(ref Simulation __instance) : base("OnDefeatEvent.Post")
			{
				this.simulation = __instance;
			}
		}
	}

}
