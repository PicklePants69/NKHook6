namespace NKHook6.Patches._TimeManager
{
    using Assets.Scripts.Utils;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._TimeManager;

	[HarmonyPatch(typeof(TimeManager), "SetFastForward")]
	class SetFastForwardHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref bool value)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new TimeManagerEvents.SetFastForwardEvent.Pre(ref value);
				EventRegistry.subscriber.dispatchEvent(ref o);
				value = o.value;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref bool value)
		{
			if (sendPostfixEvent)
			{
				var o = new TimeManagerEvents.SetFastForwardEvent.Post(ref value);
				EventRegistry.subscriber.dispatchEvent(ref o);
				value = o.value;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}
}
