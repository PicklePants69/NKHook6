using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Unity;
using NKHook6.Utils;

namespace NKHook6.Api.Extensions
{
    public static class BloonExt
    {
        public static string getId(this Bloon bloon)
        {
            return bloon.getId();
        }
        public static void setBloonModel(this Bloon bloon, BloonModel model)
        {
            bloon.model = model;
        }
        public static float getDamage(this Bloon bloon)
        {
            return bloon.GetModifiedTotalLeakDamage();
        }
        public static bool isCamo(this Bloon bloon)
        {
            if (bloon.bloonModel != null)
                return bloon.bloonModel.isCamo;
            else
                return false;
        }
        public static void setCamo(this Bloon bloon, bool isCamo)
        {
            if (bloon.bloonModel != null)
            {
                bloon.bloonModel = Toolkit.SetBloonStatus(bloon.bloonModel.name, isCamo, bloon.isFortified(), bloon.isRegrow());
            }
        }
        public static bool isRegrow(this Bloon bloon)
        {
            if (bloon.bloonModel != null)
                return bloon.bloonModel.isGrow;
            else
                return false;
        }
        public static void setRegrow(this Bloon bloon, bool isRegrow)
        {
            if (bloon.bloonModel != null)
            {
                bloon.bloonModel = Toolkit.SetBloonStatus(bloon.bloonModel.name, bloon.isCamo(), bloon.isFortified(), isRegrow);
            }
        }
        public static bool isFortified(this Bloon bloon)
        {
            if (bloon.bloonModel != null)
                return bloon.bloonModel.isFortified;
            else
                return false;
        }
        public static void setFortified(this Bloon bloon, bool isFortified)
        {
            if (bloon.bloonModel != null)
            {
                bloon.bloonModel = Toolkit.SetBloonStatus(bloon.bloonModel.name, bloon.isCamo(), isFortified, bloon.isRegrow());
            }
        }
        public static BloonModel getNextStrongest(this Bloon bloon, bool allowCamo = false, bool allowFortified = false, bool allowRegrow = false)
        {
            string bloonId = bloon.getId();
            BloonModel nextBloonModel = null;
            var allBloonTypes = Game.instance.getAllBloonModels();

            int max = allBloonTypes.Length - 1; // subtract 1 more here to avoid test bloon
            int currentBloonNum = Toolkit.GetBloonIdNum(bloonId);

            if (!allowCamo && !allowFortified && !allowRegrow)
            {
                string baseBloon = bloonId.Replace("Camo", "").Replace("Fortified", "").Replace("Regrow", "");
                for (int a = Toolkit.GetBloonIdNum(baseBloon); a < max; a++)
                {
                    if (allBloonTypes[a].name.Contains(baseBloon))
                        continue;

                    nextBloonModel = Toolkit.RemoveBloonStatus(allBloonTypes[a].name, true, true, true);
                    break;
                }
            }
            else
            {
                nextBloonModel = Toolkit.RemoveBloonStatus(allBloonTypes[currentBloonNum + 1].name, !allowCamo, !allowFortified, !allowRegrow);
            }

            return nextBloonModel;
        }
        public static BloonModel getNextWeakest(this Bloon bloon, bool allowCamo = false,
            bool allowFortified = false, bool allowRegrow = false)
        {
            var allBloonTypes = Game.instance.getAllBloonModels();

            string bloonId = bloon.getId();
            string nextBloon = bloonId;
            int max = allBloonTypes.Length - 1; // subtract 1 more here to avoid test bloon
            for (int i = 0; i < max; i++)
            {
                if (bloonId.ToLower() != allBloonTypes[i].name.ToLower())
                    continue;

                if (i == 0)
                {
                    nextBloon = allBloonTypes[0].name;
                    break;
                }
                nextBloon = allBloonTypes[i - 1].name;
                break;
            }

            var nextWeakestBloon = Toolkit.SetBloonStatus(nextBloon, allowCamo, allowFortified, allowRegrow);
            return nextWeakestBloon;
        }
    }
}
