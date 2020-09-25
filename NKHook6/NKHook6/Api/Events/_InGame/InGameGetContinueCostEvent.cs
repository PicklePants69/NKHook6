using Assets.Scripts.Utils;

namespace NKHook6.Api.Events.__InGame
{
	public class InGameGetContinueCostEvent
	{
		public class Prefix : EventBaseCancellable
		{
			public Assets.Scripts.Unity.UI_New.InGame.InGame inGame;

			public Prefix(ref Assets.Scripts.Unity.UI_New.InGame.InGame __instance) : base("InGameGetContinueCostEvent.Pre")
			{
				this.inGame = __instance;
			}
		}

		public class Postfix : EventBase
		{
			public Assets.Scripts.Unity.UI_New.InGame.InGame inGame;
			public KonFuze konFuze;

			public Postfix(ref Assets.Scripts.Unity.UI_New.InGame.InGame __instance, ref KonFuze __result) : base("InGameGetContinueCostEvent.Post")
			{
				this.inGame = __instance;
				this.konFuze = __result;
			}
		}
	}

}
