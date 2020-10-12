using MelonLoader;
using System;
using System.Threading;
using NKHook6.Api;
using static NKHook6.Logger;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Backend;
using Assets.Scripts.Unity.UI_New.Main;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame;

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

            if (Game.instance == null || InGame.instance == null || InGame.instance.bridge == null)
                return;

            NotificationMgr.CheckForNotifications();
        }
    }


    [HarmonyPatch(typeof(MainMenu), "OnEnable")]
    public class MainMenu_Patch
    {
        internal static bool checkedForUpdates = false;
        [HarmonyPostfix]
        internal static void Postfix(MainMenu __instance)
        {
            
        }
    }
}
