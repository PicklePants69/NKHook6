using Assets.Scripts.Unity.UI_New.Main;
namespace NKHook6.Api.Events._MainMenu
{
    public class OnEnableEvent
    {
        public class Pre : EventBaseCancellable
        {
            public MainMenu instance;

            public Pre(MainMenu __instance) : base("MainMenu.OnEnable.Pre")
            {
                this.instance = __instance;
            }
        }
        public class Post : EventBase
        {
            public MainMenu instance;
            public Post(MainMenu __instance) : base("MainMenu.OnEnable.Post")
            {
                this.instance = __instance;
            }
        }
    }
}
