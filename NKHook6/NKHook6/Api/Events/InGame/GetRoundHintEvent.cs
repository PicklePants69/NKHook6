using Assets.Scripts.Unity.UI_New.InGame;

namespace NKHook6.Api.Events._InGame
{
	public class GetRoundHintEvent
	{
		public class Pre : EventBaseCancellable
		{
			public InGame instance;

			public Pre(ref InGame __instance) : base("InGame.GetRoundHintEvent.Pre")
			{
				this.instance = __instance;
			}
		}

		public class Post : EventBase
		{
			public InGame instance;
			public string result;

			public Post(ref InGame __instance, ref string __result) : base("InGame.GetRoundHintEvent.Post")
			{
				this.instance = __instance;
				this.result = __result;
			}
		}
	}

}
