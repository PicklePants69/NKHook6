using Assets.Scripts.Unity.UI_New.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Events
{
    public class MainMenuShownEvent
    {
        public class Prefix : EventBase
        {
            MainMenu __instance;
            public Prefix(MainMenu __instance) : base("MainMenuShownEvent.Pre")
            {
                this.__instance = __instance;
            }
        }
        public class Postfix : EventBase
        {
            MainMenu __instance;
            public Postfix(MainMenu __instance) : base("MainMenuShownEvent.Post")
            {
                this.__instance = __instance;
            }
        }
    }
}
