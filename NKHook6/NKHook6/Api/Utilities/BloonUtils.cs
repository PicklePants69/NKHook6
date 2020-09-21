using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Unity;
using NKHook6.Api.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NKHook6.Api.Utilities
{
    public class BloonUtils
    {
        public static void ChangeNextBloonTo(DefaultBloonIds bloonId) => ChangeNextBloonTo(bloonId.ToString());
        public static void ChangeNextBloonTo(string bloonId) => ChangeNextBloonTo(BloonUtils.GetBloon(bloonId));
        public static void ChangeNextBloonTo(BloonModel bloonModel) => OnBloonSpawned.changeBloonToThisModel = bloonModel;


        public static BloonModel GetBloon(DefaultBloonIds bloonId) => GetBloon(bloonId.ToString());

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
