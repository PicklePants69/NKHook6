using Assets.Scripts.Unity.UI_New.Main;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events._MainMenu;

namespace NKHook6.Backend.Patches._MainMenu
{
    [HarmonyPatch(typeof(MainMenu), "Open")]
    class LoadHook
    {
        private static bool sendEvent = true;

        [HarmonyPostfix]
        internal static void Postfix(MainMenu __instance, Il2CppSystem.Object data)
        {
            if (sendEvent)
            {
                var o = new MainMenuEvents.LoadedEvent(__instance);
                EventRegistry.instance.dispatchEvent(ref o);
            }
            sendEvent = !sendEvent;
        }
    }
}
