using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.SMath;
using Mono.CSharp;
using NKHook6.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Extensions
{
    public static class BloonExt
    {
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
                bloon.bloonModel = BloonUtils.SetBloonStatus(bloon.bloonModel.name, isCamo, bloon.isFortified(), bloon.isRegrow());
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
                bloon.bloonModel = BloonUtils.SetBloonStatus(bloon.bloonModel.name, bloon.isCamo(), bloon.isFortified(), isRegrow);
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
                bloon.bloonModel = BloonUtils.SetBloonStatus(bloon.bloonModel.name, bloon.isCamo(), isFortified, bloon.isRegrow());
            }
        }
        public static BloonModel getNextStrongest(this Bloon bloon)
        {
            return BloonUtils.GetNextStrongestBloon(bloon.bloonModel.baseId, bloon.isCamo(), bloon.isFortified(), bloon.isRegrow());
        }
        public static BloonModel getNextWeakest(this Bloon bloon)
        {
            return BloonUtils.GetNextWeakestBloon(bloon.bloonModel.baseId, bloon.isCamo(), bloon.isFortified(), bloon.isRegrow());
        }
    }
}
