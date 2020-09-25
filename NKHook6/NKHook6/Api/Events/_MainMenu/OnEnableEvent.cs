using Assets.Scripts.Unity.UI_New.Main;
namespace NKHook6.Api.Events._MainMenu
{
    public class OnEnableEvent
    {
        public class Pre : EventBaseCancellable
        {
            public MainMenu mainMenu;

            public Pre(MainMenu __instance) : base("MainMenu.OnEnable.Pre")
            {
                this.mainMenu = __instance;
            }
        }
        public class Post : EventBase
        {
            public MainMenu mainMenu;
            public Post(MainMenu __instance) : base("MainMenu.OnEnable.Post")
            {
                this.mainMenu = __instance;
            }
        }
    }
}
