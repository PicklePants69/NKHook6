using Assets.Scripts.Models.Profile;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Patches._ProfileModel
{
    [HarmonyPatch(typeof(ProfileModel), "Validate")]
    class ProfilePatch
    {
        internal static ProfileModel theModel;
        [HarmonyPostfix]
        internal static void Postfix(ProfileModel __instance)
        {
            ProfilePatch.theModel = __instance;
        }
    }
}
