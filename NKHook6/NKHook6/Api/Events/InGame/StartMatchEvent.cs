using Assets.Scripts.Unity.UI_New.InGame;

namespace NKHook6.Api.Events._InGame
{
	public partial class InGameEvents
	{
		public class StartMatchEvent : EventBaseCancellable
		{
			public InGame inGame;

			public VictoryEvent(ref InGame inGame) : base("StartMatchEvent")
			{
				this.inGame = inGame;
			}
		}
	}
}
