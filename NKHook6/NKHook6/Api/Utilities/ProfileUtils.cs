using Assets.Scripts.Models.Profile;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.TowerSets;
using Assets.Scripts.Unity.Player;
using Harmony;
using Il2CppSystem.Collections.Generic;

namespace NKHook6.Api.Utilities
{
    public class ProfileUtils
    {
        public static List<string> UnlockTheseTowers;
        public static List<string> LockTheseTowers;
        public static ProfileModel profileModel;
        public static Btd6Player btd6Player;

    }

    [HarmonyPatch(typeof(ProfileModel), "Validate")] // this method is called after the profile data is parsed, hence why it's used to modify said profile data
    internal class ProfileModel_Patch
    {
        [HarmonyPostfix]
        internal static void Postfix(ProfileModel __instance)
        {
            ProfileUtils.profileModel = __instance;

            if ((ProfileUtils.UnlockTheseTowers == null || ProfileUtils.UnlockTheseTowers.Count == 0) &&
                (TowerUtils.AddTheseTowersToList == null || TowerUtils.AddTheseTowersToList.Count == 0))
                return;

            List<string> unlockedTowers = __instance.unlockedTowers;

            if (TowerUtils.AddTheseTowersToList != null && TowerUtils.AddTheseTowersToList.Count > 0)
            {
                foreach (var item in TowerUtils.AddTheseTowersToList)
                {
                    if (unlockedTowers.Contains(item.key.towerId))
                        continue;

                    unlockedTowers.Add(item.key.towerId);
                }
            }


            if (ProfileUtils.UnlockTheseTowers != null && ProfileUtils.UnlockTheseTowers.Count > 0)
            {
                foreach (var item in ProfileUtils.UnlockTheseTowers)
                {
                    if (unlockedTowers.Contains(item))
                        continue;

                    unlockedTowers.Add(item);
                }
            }

            if (ProfileUtils.LockTheseTowers != null && ProfileUtils.LockTheseTowers.Count > 0)
            {
                foreach (var item in ProfileUtils.LockTheseTowers)
                {
                    if (!unlockedTowers.Contains(item))
                        continue;

                    unlockedTowers.Remove(item);
                }
            }
        }
    }


    [HarmonyPatch(typeof(Btd6Player), "GetAnalyticsInfo")]
    public class PlayerInfoExtensions_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Btd6Player __instance)
        {
            ProfileUtils.btd6Player = __instance;
        }
    }
}
