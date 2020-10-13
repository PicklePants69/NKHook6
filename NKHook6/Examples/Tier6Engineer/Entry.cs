using Assets.Scripts.Simulation.Towers;
using MelonLoader;
using NKHook6;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Towers;
using NKHook6.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tier6Engineer
{
    public class Entry : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            EventRegistry.subscriber.register(typeof(Entry));
        }

        [EventAttribute("TowerUpgradeEvent")]
        public static void onUpgrade(TowerEvents.UpgradeEvent e)
        {
            Tower t = e.tower;
            Logger.Log("On upgrade: " + t.towerModel.name);
            if (t.towerModel.name.Contains("EngineerMonkey"))
            {
                int[] upgrades = t.getUpgrades();
                if (upgrades.Contains(5))
                {
                    Logger.Log("Tier 5 upgrade");
                }
            }
        }
    }
}
