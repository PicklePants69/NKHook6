using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Towers;


namespace NKHook6.Patches._Towers
{
	[HarmonyPatch(typeof(Tower), "OnUpgraded")]
	class UpgradeHook
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref Tower __instance)
		{
			bool allowOriginalMethod = true;
			var o = new TowerEvents.UpgradeEvent(ref __instance);
			EventRegistry.instance.dispatchEvent(ref o);
			allowOriginalMethod = !o.isCancelled();
			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Tower __instance)
		{
			/*if (sendPostfixEvent)
			{
				var o = new TowerEvents.OnUpgradeEvent.Post(ref __instance);
				EventRegistry.instance.dispatchEvent(ref o);
				__instance = o.instance;
			}

			sendPostfixEvent = !sendPostfixEvent;*/
		}
	}

}
