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
        public class Prefix : EventBaseCancellable
        {
            public MainMenu mainMenu;

            public Prefix(MainMenu __instance) : base("MainMenuShownEvent.Pre")
            {
                this.mainMenu = __instance;
            }
        }
        public class Postfix : EventBase
        {
            public MainMenu mainMenu;
            public Postfix(MainMenu __instance) : base("MainMenuShownEvent.Post")
            {
                this.mainMenu = __instance;
            }
        }
    }
}
