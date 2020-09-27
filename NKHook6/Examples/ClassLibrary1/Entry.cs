using MelonLoader;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Bloons;
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

        [EventAttribute("Bloon.LeakedEvent.Pre")]
        public static void onLeaked(ref BloonEvents.LeakedEvent e)
        {

        }
    }
}
