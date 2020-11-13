using Assets.Scripts.Unity.UI_New.InGame;

namespace NKHook6.Api.Events._InGame
{
	public partial class InGameEvents
	{
		public class StartMatchEvent : EventBaseCancellable
		{
			public InGame inGame;
			public bool isFromSave;

			public StartMatchEvent(ref InGame inGame, ref bool isFromSave) : base("StartMatchEvent")
			{
				this.inGame = inGame;
				this.isFromSave = isFromSave;
			}
		}
	}
}
