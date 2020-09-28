using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Bloons;
using NKHook6.Api.Events._MainMenu;
using NKHook6.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Examples.LHCM
{
    public class Entry : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            EventRegistry.subscriber.register(typeof(Entry));
        }

        [EventAttribute("Bloon.LeakedEvent.Post")]
        public static void onLeaked(ref BloonEvents.LeakedEvent.Post e)
        {
            float damage = e.instance.getDamage();
            InGame inst = InGame.instance;
            if (inst != null)
            {
                double cash = inst.getCash();
                inst.setCash(cash - damage);
                cash = inst.getCash();
                if(cash > 0)
                {
                    inst.setHealth(inst.getHealth() + damage);
                }
                else
                {
                    inst.setHealth(0);
                }
            }
            return;
        }
    }
}
