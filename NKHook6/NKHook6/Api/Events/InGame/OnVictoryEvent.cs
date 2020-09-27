using Assets.Scripts.Unity.UI_New.InGame;

namespace NKHook6.Api.Events._InGame
{
	public partial class InGameEvents
	{
		public class OnVictoryEvent
		{
			public class Pre : EventBaseCancellable
			{
				public InGame instance;

				public Pre(ref InGame __instance) : base("InGame.OnVictoryEvent.Pre")
				{
					this.instance = __instance;
				}
			}

			public class Post : EventBase
			{
				public InGame instance;

				public Post(ref InGame __instance) : base("InGame.OnVictoryEvent.Post")
				{
					this.instance = __instance;
				}
			}
		}
	}
}
