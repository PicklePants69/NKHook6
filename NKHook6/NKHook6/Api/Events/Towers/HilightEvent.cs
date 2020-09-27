﻿using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events._Towers
{
	public partial class TowerEvents
	{
		public class HilightEvent
		{
			public class Pre : EventBaseCancellable
			{
				public Tower instance;

				public Pre(ref Tower __instance) : base("Tower.HilightEvent.Pre")
				{
					this.instance = __instance;
				}
			}

			public class Post : EventBase
			{
				public Tower instance;

				public Post(ref Tower __instance) : base("Tower.HilightEvent.Post")
				{
					this.instance = __instance;
				}
			}
		}
	}
}