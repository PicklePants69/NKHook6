using MelonLoader;
using System;
using System.Threading;
using NKHook6.Api;
using static NKHook6.Logger;
using NKHook6.Scripting;
using Harmony;
using NKHook6.Api.Events;
using Assets.Scripts.Simulation.Bloons;
using NKHook6.Api.Extensions;
using NKHook6.Api.Utilities;
using Assets.Scripts.Unity.UI_New.InGame;
using NKHook6.Backend;

namespace NKHook6
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            
            //BTD_Backend.Log.MessageLogged += Log_MessageLogged;
            Logger.Log("NKHook6 is initializing...");
            Log("CWD: " + Environment.CurrentDirectory);

            InitializeHarmony();

            new EventRegistry();
            new KeyListener();

            EventRegistry.subscriber.register(this.GetType());
            InitializeBoo();
            Log("NKHook6 initialized");

            InitializeCommandMgr();
            

        }

        [EventAttribute("MainMenu.OnEnableEvent.Post")]
        public static void MainMenuShown(NKHook6.Api.Events._MainMenu.OnEnableEvent.Post e)
        {
            //BgWorker bg = new BgWorker();
            NkhUpdateMgr.HandleUpdates();
        }


        private void InitializeBoo()
        {
            Log("Initializing Boo...");
            BooManager.ExecuteAllScripts();
            Log("Initialized Boo");
        }

        private void InitializeHarmony()
        {
            Log("Initializing Harmony...");
            HarmonyInstance.Create("TD Toolbox.NKHook6").PatchAll();
            Log("Finished Initializing Harmony. Hooks are patched");
        }

        private void InitializeCommandMgr()
        {
            new Thread(() =>
            {
                while (true)
                {
                    string input = Console.ReadLine();
                    CommandManager.onCommand(input);
                }
            }).Start();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            UpdateEvent update = new UpdateEvent();
            EventRegistry.subscriber.dispatchEvent(ref update);
        }
    }
}
