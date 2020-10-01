using MelonLoader;
using System;
using System.Threading;
using NKHook6.Api;
using static NKHook6.Logger;
using NKHook6.Scripting;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Backend;
using NKHook6.Api.Events._MainMenu;

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

            new EventRegistry();
            new KeyListener();

            EventRegistry.subscriber.register(typeof(Main));

            Log("NKHook6 initialized");

            InitializeCommandMgr();
            

        }

        [EventAttribute("MainMenuLoadedEvent")]
        public static void MainMenuShown(MainMenuEvents.LoadedEvent e)
        {
            Logger.Log("Async works too i guess");
            UpdateMgr.HandleUpdates();
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
