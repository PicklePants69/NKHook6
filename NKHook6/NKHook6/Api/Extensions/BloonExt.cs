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
            return bloon.bloonModel.id;
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
            return bloon.bloonModel.isCamo();
        }

        public static void setCamo(this Bloon bloon, bool isCamo)
        {
            bloon.bloonModel.setCamo(isCamo);
        }

        public static bool isRegrow(this Bloon bloon)
        {
            return bloon.bloonModel.isRegrow();
        }

        public static void setRegrow(this Bloon bloon, bool isRegrow)
        {
            bloon.bloonModel.setRegrow(isRegrow);
        }

        public static bool isFortified(this Bloon bloon)
        {
            return bloon.bloonModel.isFortified();
        }

        public static void setFortified(this Bloon bloon, bool isFortified)
        {
            bloon.bloonModel.setFortified(isFortified);
        }
    }
}
