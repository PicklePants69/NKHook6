using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Unity;
using NKHook6.Utils;
using System;

namespace NKHook6.Api.Extensions
{
    public static class BloonModelExt
    {
        private static void ThrowIfBloonModelIsNull(BloonModel bloonModel, string exceptionMsg)
        {
            if (bloonModel == null)
                throw new ArgumentException(exceptionMsg, "bloonModel");
        }

        public static bool isCamo(this BloonModel bloonModel)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to check if bloonModel is camo because it was null");

            return bloonModel.isCamo;
        }

        public static void setCamo(this BloonModel bloonModel, bool isCamo)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to set bloonModel to camo because it was null");

            bloonModel = Toolkit.SetBloonStatus(bloonModel.name, isCamo, bloonModel.isFortified(), bloonModel.isRegrow());
        }

        public static bool isRegrow(this BloonModel bloonModel)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to check if bloonModel is regrow because it was null");

            return bloonModel.isGrow;
        }

        public static void setRegrow(this BloonModel bloonModel, bool isRegrow)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to set bloonModel to regrow because it was null");

            bloonModel = Toolkit.SetBloonStatus(bloonModel.name, bloonModel.isCamo(), bloonModel.isFortified(), isRegrow);
        }

        public static bool isFortified(this BloonModel bloonModel)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to check if bloonModel is fortified because it was null");

            return bloonModel.isFortified;
        }

        public static void setFortified(this BloonModel bloonModel, bool isFortified)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to set bloonModel to fortified because it was null");

            bloonModel = Toolkit.SetBloonStatus(bloonModel.name, bloonModel.isCamo(), isFortified, bloonModel.isRegrow());
        }

        public static BloonModel getNextStrongest(this BloonModel bloonModel, bool allowCamo = false, bool allowFortified = false, bool allowRegrow = false)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to get next strongest bloon because the base bloonModel was null");

            string bloonId = bloonModel.id;
            BloonModel nextBloonModel = null;
            var allBloonTypes = Game.instance.getAllBloonModels();

            int max = allBloonTypes.Count - 1; // subtract 1 more here to avoid test bloon
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
        public static BloonModel getNextWeakest(this BloonModel bloonModel, bool allowCamo = false,
            bool allowFortified = false, bool allowRegrow = false)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to get next weakest bloon because the base bloonModel was null");

            var allBloonTypes = Game.instance.getAllBloonModels();

            string bloonId = bloonModel.id;
            string nextBloon = bloonId;
            int max = allBloonTypes.Count - 1; // subtract 1 more here to avoid test bloon
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
