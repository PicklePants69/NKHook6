using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Unity;
using NKHook6.Api.Events.Bloons;
using NKHook6.Api.Enums;
using System;
using Boo.Lang;
using Assets.Scripts.Simulation.Bloons;

namespace NKHook6.Api.Utilities
{
    public class BloonUtils
    {
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
        /// Get the next strongest bloon. Ex: the next strongest bloon after Red is Red Regrow
        /// </summary>
        /// <param name="bloonId">The bloon id of the current bloon. Ex: Red</param>
        /// <param name="allowCamo">Is it okay if the next bloon is a camo bloon. Ex: Red => RedCamo</param>
        /// <param name="allowFortified">Is it okay if the next bloon is a Fortified bloon. Ex: Red => RedFortified</param>
        /// <param name="allowRegrow">Is it okay if the next bloon is a Regrow bloon. Ex: Red => RedRegrow</param>
        /// <returns>The next strongest bloon</returns>
        public static BloonModel GetNextStrongestBloon(DefaultBloonIds bloonId, bool allowCamo = true, 
            bool allowFortified = true, bool allowRegrow = true) => GetNextStrongestBloon(bloonId.ToString());


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
            int max = allBloonTypes.Count - 2; //Subtracting 2 to avoid testbloon
            for (int i = 0; i < max; i++)
            {
                if (bloonId.ToLower() != allBloonTypes[i].ToLower())
                    continue;
                nextBloon = allBloonTypes[i];
            }

            if (nextBloon.Contains("Camo") && allowCamo == false)
                nextBloon = nextBloon.Replace("Camo", "");
            if (nextBloon.Contains("Fortified") && allowFortified == false)
                nextBloon = nextBloon.Replace("Fortified", "");
            if (nextBloon.Contains("Regrow") && allowRegrow == false)
                nextBloon = nextBloon.Replace("Regrow", "");

            return GetBloon(nextBloon);
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
