using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Unity;
using NKHook6.Api.Events._Bloons;
using NKHook6.Api.Enums;
using System;
using Assets.Scripts.Simulation.Bloons;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NKHook6.Api.Utilities
{
    public class BloonUtils
    {
        /// <summary>
        /// A list of all bloons currently on the map. Updated when a bloon is initialised or destroyed
        /// </summary>
        public static List<Bloon> BloonsOnMap = new List<Bloon>();

        /// <summary>
        /// Get a list of all of the default bloon ids. Doesn't get custom bloons
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllBloonTypes()
        {
            var allBloonTypes = new List<string>();
            foreach (var item in EnumUtils.GetValues<DefaultBloonIds>())
                allBloonTypes.Add(item.ToString());

            return allBloonTypes;
        }


        /// <summary>
        /// Get the number ID of the bloon. Mainly used to get the numeric position of bloon in the list of DefaultBloonIds
        /// </summary>
        /// <param name="bloonId">bloon name of bloon you want Id for</param>
        /// <returns></returns>
        public static int GetBloonIdNum(string bloonId)
        {
            int result = 0;
            var allBloons = GetAllBloonTypes();
            for (int i = 0; i < allBloons.Count; i++)
            {
                if (allBloons[i].ToLower() != bloonId.ToLower())
                    continue;

                result = i;
                break;
            }

            return result;
        }


        public static BloonModel SetBloonStatus(string bloonId, [Optional]bool setCamo, [Optional]bool setFortified, [Optional]bool setRegrow)
        {
            string camoText = "";
            string fortifiedText = "";
            string regrowText = "";
            string bloonBase = bloonId.Replace("Camo", "").Replace("Fortified", "").Replace("Regrow", "");

            if (setCamo || (bloonId.Contains("Camo")))
            {
                if (GetBloon(bloonBase + "Camo", true) != null)
                    camoText = "Camo";
            }
            if (setFortified || (bloonId.Contains("Fortified")))
            {
                if (GetBloon(bloonBase + "Fortified", true) != null)
                    fortifiedText = "Fortified";
            }
            if (setRegrow || (bloonId.Contains("Regrow")))
            {
                if (GetBloon(bloonBase + "Regrow", true) != null)
                    regrowText = "Regrow";
            }

            string newBloonID = bloonBase + regrowText + fortifiedText + camoText;
            var newBloon = GetBloon(newBloonID, true);

            return newBloon;
        }


        /// <summary>
        /// Get the next strongest bloon. Ex: the next strongest bloon after Red is Red Regrow
        /// </summary>
        /// <param name="bloon">The bloon id of the current bloon. Ex: Red</param>
        /// <param name="allowCamo">Is it okay if the next bloon is a camo bloon. Ex: Red => RedCamo</param>
        /// <param name="allowFortified">Is it okay if the next bloon is a Fortified bloon. Ex: Red => RedFortified</param>
        /// <param name="allowRegrow">Is it okay if the next bloon is a Regrow bloon. Ex: Red => RedRegrow</param>
        /// <returns>The next strongest bloon</returns>
        public static BloonModel GetNextStrongestBloon(BloonModel bloon, bool allowCamo = true,
            bool allowFortified = true, bool allowRegrow = true, bool ignoreException = true) => GetNextStrongestBloon(bloon.name,
                allowCamo, allowFortified, allowRegrow, ignoreException);

        /// <summary>
        /// Get the next strongest bloon. Ex: the next strongest bloon after Red is Red Regrow
        /// </summary>
        /// <param name="bloonId">The bloon id of the current bloon. Ex: Red</param>
        /// <param name="allowCamo">Is it okay if the next bloon is a camo bloon. Ex: Red => RedCamo</param>
        /// <param name="allowFortified">Is it okay if the next bloon is a Fortified bloon. Ex: Red => RedFortified</param>
        /// <param name="allowRegrow">Is it okay if the next bloon is a Regrow bloon. Ex: Red => RedRegrow</param>
        /// <returns>The next strongest bloon</returns>
        public static BloonModel GetNextStrongestBloon(DefaultBloonIds bloonId, bool allowCamo = true, 
            bool allowFortified = true, bool allowRegrow = true, bool ignoreException = true) => GetNextStrongestBloon(bloonId.ToString(), 
                allowCamo, allowFortified, allowRegrow, ignoreException);


        /// <summary>
        /// Get the next strongest bloon. Ex: the next strongest bloon after Red is Red Regrow
        /// </summary>
        /// <param name="bloonId">The bloon id of the current bloon. Ex: Red</param>
        /// <param name="allowCamo">Is it okay if the next bloon is a camo bloon. Ex: Red => RedCamo</param>
        /// <param name="allowFortified">Is it okay if the next bloon is a Fortified bloon. Ex: Red => RedFortified</param>
        /// <param name="allowRegrow">Is it okay if the next bloon is a Regrow bloon. Ex: Red => RedRegrow</param>
        /// <returns>The next strongest bloon</returns>
        public static BloonModel GetNextStrongestBloon(string bloonId, bool allowCamo = true,
            bool allowFortified = true, bool allowRegrow = true, bool ignoreException = true)
        {
            BloonModel nextBloonModel = null;
            var allBloonTypes = GetAllBloonTypes();

            int max = allBloonTypes.Count - 1; // subtract 1 more here to avoid test bloon
            int currentBloonNum = GetBloonIdNum(bloonId);

            if (!allowCamo && !allowFortified && !allowRegrow)
            {
                string baseBloon = bloonId.Replace("Camo", "").Replace("Fortified", "").Replace("Regrow", "");
                for (int a = GetBloonIdNum(baseBloon); a < max; a++)
                {
                    if (allBloonTypes[a].Contains(baseBloon))
                        continue;
                    
                    nextBloonModel = RemoveBloonStatus(allBloonTypes[a], true, true, true, ignoreException);
                    break;
                }
            }
            else
            {
                nextBloonModel = RemoveBloonStatus(allBloonTypes[currentBloonNum + 1], !allowCamo, !allowFortified, !allowRegrow, ignoreException);
            }

            return nextBloonModel;
        }
        
        /// <summary>
        /// Get the next weakest bloon. Ex: the next strongest bloon after Red is Red Regrow
        /// </summary>
        /// <param name="bloonId">The bloon id of the current bloon. Ex: Red</param>
        /// <param name="allowCamo">Is it okay if the next bloon is a camo bloon. Ex: Red => RedCamo</param>
        /// <param name="allowFortified">Is it okay if the next bloon is a Fortified bloon. Ex: Red => RedFortified</param>
        /// <param name="allowRegrow">Is it okay if the next bloon is a Regrow bloon. Ex: Red => RedRegrow</param>
        /// <returns>The next strongest bloon</returns>
        public static BloonModel GetNextWeakestBloon(string bloonId, bool allowCamo = true,
            bool allowFortified = true, bool allowRegrow = true)
        {
            var allBloonTypes = GetAllBloonTypes();

            string nextBloon = bloonId;
            int max = allBloonTypes.Count - 1; // subtract 1 more here to avoid test bloon
            for (int i = 0; i < max; i++)
            {
                if (bloonId.ToLower() != allBloonTypes[i].ToLower())
                    continue;

                if (i == 0)
                {
                    nextBloon = allBloonTypes[0];
                    break;
                }
                nextBloon = allBloonTypes[i-1];
                break;
            }

            var nextWeakestBloon = SetBloonStatus(nextBloon, allowCamo, allowFortified, allowRegrow);
            return nextWeakestBloon;
        }


        /// <summary>
        /// Get the BloonModel of the bloonId you enter
        /// </summary>
        /// <param name="bloonId">The ID of the bloon you want</param>
        /// <returns></returns>
        public static BloonModel GetBloon(DefaultBloonIds bloonId, bool ignoreException = false) => GetBloon(bloonId.ToString(), ignoreException);


        /// <summary>
        /// Get the BloonModel of the bloonId you enter
        /// </summary>
        /// <param name="bloonId">The ID of the bloon you want</param>
        /// <returns></returns>
        public static BloonModel GetBloon(string bloonId, bool ignoreException = false)
        {
            BloonModel result = null;

            if (Game.instance == null)
            {
                Logger.Log("Can't get BloonModel for BloonId: \"" + bloonId + "\". The Game instance is null");
                return result;
            }
            else if (Game.instance.model == null)
            {
                Logger.Log("Can't get BloonModel for BloonId: \"" + bloonId + "\". The Game instance model is null");
                return result;
            }

            try
            {
                result = Game.instance.model.GetBloon(bloonId);
            }
            catch (Exception e)
            {
                if (ignoreException)
                    return result;

                Logger.Log("Exception occured when trying to use GetBloon from NKHook6." +
                    " Tried Getting a bloon with this non-existant BloonId: \"" + bloonId + "\"." +
                    "\nMore exception details: " + e.Message);
            }

            return result;
        }


        public static BloonModel RemoveBloonStatus(string bloonId, bool removeCamo, bool removeFortified, bool removeRegrow, bool ignoreException = true)
        {
            if (bloonId.Contains("Camo") && removeCamo)
                bloonId = bloonId.Replace("Camo", "");
            if (bloonId.Contains("Fortified") && removeFortified)
                bloonId = bloonId.Replace("Fortified", "");
            if (bloonId.Contains("Regrow") && removeRegrow)
                bloonId = bloonId.Replace("Regrow", "");

            var bloon = GetBloon(bloonId, ignoreException);
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
