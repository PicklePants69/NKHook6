using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Unity;
using NKHook6.Api.Events._Bloons;
using NKHook6.Api.Enums;
using System;
using Assets.Scripts.Simulation.Bloons;
using System.Collections.Generic;

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

        /// <summary>
        /// Remove status effects from the bloon, like camo and regen
        /// </summary>
        /// <param name="bloonId">The ID of the bloon you want to modify</param>
        /// <param name="allowCamo"></param>
        /// <param name="allowFortified"></param>
        /// <param name="allowRegrow"></param>
        /// <returns></returns>
        public static BloonModel RemoveBloonStatus(string bloonId, bool allowCamo, bool allowFortified, bool allowRegrow)
        {
            if (bloonId.Contains("Camo") && allowCamo == false)
                bloonId = bloonId.Replace("Camo", "");
            if (bloonId.Contains("Fortified") && allowFortified == false)
                bloonId = bloonId.Replace("Fortified", "");
            if (bloonId.Contains("Regrow") && allowRegrow == false)
                bloonId = bloonId.Replace("Regrow", "");

            var bloon = GetBloon(bloonId);
            if (bloon == null)
            {
                Logger.Log("Failed to remove status from bloon. It returned null");
                return null;
            }

            return bloon;
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
            bool allowFortified = true, bool allowRegrow = true) => GetNextStrongestBloon(bloon.name,
                allowCamo, allowFortified, allowRegrow);

        /// <summary>
        /// Get the next strongest bloon. Ex: the next strongest bloon after Red is Red Regrow
        /// </summary>
        /// <param name="bloonId">The bloon id of the current bloon. Ex: Red</param>
        /// <param name="allowCamo">Is it okay if the next bloon is a camo bloon. Ex: Red => RedCamo</param>
        /// <param name="allowFortified">Is it okay if the next bloon is a Fortified bloon. Ex: Red => RedFortified</param>
        /// <param name="allowRegrow">Is it okay if the next bloon is a Regrow bloon. Ex: Red => RedRegrow</param>
        /// <returns>The next strongest bloon</returns>
        public static BloonModel GetNextStrongestBloon(DefaultBloonIds bloonId, bool allowCamo = true, 
            bool allowFortified = true, bool allowRegrow = true) => GetNextStrongestBloon(bloonId.ToString(), 
                allowCamo, allowFortified, allowRegrow);


        /// <summary>
        /// Get the next strongest bloon. Ex: the next strongest bloon after Red is Red Regrow
        /// </summary>
        /// <param name="bloonId">The bloon id of the current bloon. Ex: Red</param>
        /// <param name="allowCamo">Is it okay if the next bloon is a camo bloon. Ex: Red => RedCamo</param>
        /// <param name="allowFortified">Is it okay if the next bloon is a Fortified bloon. Ex: Red => RedFortified</param>
        /// <param name="allowRegrow">Is it okay if the next bloon is a Regrow bloon. Ex: Red => RedRegrow</param>
        /// <returns>The next strongest bloon</returns>
        public static BloonModel GetNextStrongestBloon(string bloonId, bool allowCamo = true,
            bool allowFortified = true, bool allowRegrow = true)
        {
            var allBloonTypes = GetAllBloonTypes();

            string nextBloon = bloonId;
            int max = allBloonTypes.Count - 1; // subtract 1 more here to avoid test bloon
            for (int i = 0; i < max; i++)
            {
                if (bloonId.ToLower() != allBloonTypes[i].ToLower())
                    continue;

                nextBloon = allBloonTypes[i];
            }

            return RemoveBloonStatus(nextBloon, allowCamo, allowFortified, allowRegrow);
        }


        /// <summary>
        /// Get the BloonModel of the bloonId you enter
        /// </summary>
        /// <param name="bloonId">The ID of the bloon you want</param>
        /// <returns></returns>
        public static BloonModel GetBloon(DefaultBloonIds bloonId) => GetBloon(bloonId.ToString());


        /// <summary>
        /// Get the BloonModel of the bloonId you enter
        /// </summary>
        /// <param name="bloonId">The ID of the bloon you want</param>
        /// <returns></returns>
        public static BloonModel GetBloon(string bloonId)
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
                Logger.Log("Exception occured when trying to use GetBloon from NKHook6." +
                    " Tried Getting a bloon with this non-existant BloonId: \"" + bloonId + "\"." +
                    "\nMore exception details: " + e.Message);
            }

            return result;
        }
    }
}
