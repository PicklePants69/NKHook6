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
            MainMenu menu;
            public Prefix(MainMenu __instance) : base("MainMenuShownEvent.Pre")
            {
                this.menu = __instance;
            }
        }
        public class Postfix : EventBase
        {
            MainMenu menu;
            public Postfix(MainMenu __instance) : base("MainMenuShownEvent.Post")
            {
                this.menu = __instance;
            }
        }
    }
}
