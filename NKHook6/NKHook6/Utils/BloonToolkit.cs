using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Unity;
using NKHook6.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Utils
{
    internal partial class Toolkit
    {

        /// <summary>
        /// Get the number ID of the bloon. Mainly used to get the numeric position of bloon in the list of DefaultBloonIds
        /// </summary>
        /// <param name="bloonId">bloon name of bloon you want Id for</param>
        /// <returns></returns>
        public static int GetBloonIdNum(string bloonId)
        {
            var allBloons = Game.instance.model.bloons;
            for (int i = 0; i < allBloons.Count; i++)
            {
                if (allBloons[i].name.ToLower() != bloonId.ToLower())
                    continue;
                return i;
            }
            return -1;
        }


        public static BloonModel SetBloonStatus(string bloonId, [Optional] bool setCamo, [Optional] bool setFortified, [Optional] bool setRegrow)
        {
            string camoText = "";
            string fortifiedText = "";
            string regrowText = "";
            string bloonBase = bloonId.Replace("Camo", "").Replace("Fortified", "").Replace("Regrow", "");

            if (setCamo || (bloonId.Contains("Camo")))
            {
                if (Game.instance.getBloonModel(bloonBase + "Camo") != null)
                    camoText = "Camo";
            }
            if (setFortified || (bloonId.Contains("Fortified")))
            {
                if (Game.instance.getBloonModel(bloonBase + "Fortified") != null)
                    fortifiedText = "Fortified";
            }
            if (setRegrow || (bloonId.Contains("Regrow")))
            {
                if (Game.instance.getBloonModel(bloonBase + "Regrow") != null)
                    regrowText = "Regrow";
            }

            string newBloonID = bloonBase + regrowText + fortifiedText + camoText;
            var newBloon = Game.instance.getBloonModel(newBloonID);

            return newBloon;
        }


        public static BloonModel RemoveBloonStatus(string bloonId, bool removeCamo, bool removeFortified, bool removeRegrow, bool ignoreException = true)
        {
            if (bloonId.Contains("Camo") && removeCamo)
                bloonId = bloonId.Replace("Camo", "");
            if (bloonId.Contains("Fortified") && removeFortified)
                bloonId = bloonId.Replace("Fortified", "");
            if (bloonId.Contains("Regrow") && removeRegrow)
                bloonId = bloonId.Replace("Regrow", "");

            var bloon = Game.instance.getBloonModel(bloonId);
            if (bloon == null)
            {
                if (!ignoreException)
                    Logger.Log("Failed to remove status from bloon. It returned null");
                return null;
            }

            return bloon;
        }
    }
}
