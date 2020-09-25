using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Utils;

namespace NKHook6.Api.Events.__InGame
{
	public class GetContinueCostEvent
	{
		public class Pre : EventBaseCancellable
		{
			public InGame inGame;

			public Pre(ref InGame __instance) : base("InGame.GetContinueCostEvent.Pre")
			{
				this.inGame = __instance;
			}
		}

		public class Post : EventBase
		{
			public Assets.Scripts.Unity.UI_New.InGame.InGame inGame;
			public KonFuze konFuze;

			public Post(ref InGame __instance, ref KonFuze __result) : base("InGame.GetContinueCostEvent.Post")
			{
				this.inGame = __instance;
				this.konFuze = __result;
			}
		}
	}

}
