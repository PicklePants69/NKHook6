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
using UnityEngine;
using Novell.Directory.Ldap.Rfc2251;
using NKHook6.Api.Extensions;
using Assets.Main.Scenes;
using Assets.Main;
using NKHook6.Api.Gamemodes;

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

        private static AssetBundle assetBundle;
        private static GameObject canvas;
        private static GameObject gameObject;

        private static bool done = false;
        [HarmonyPostfix]
        internal static void Postfix(MainMenu __instance)
        {
            //var r = new System.Random();
            //Game.instance.setMonkeyMoney(r.Next(5000, 12350));

            Loader.Start();
        }
    }


    
    [HarmonyPatch(typeof(InitialLoadingScreen), "Start")]
    internal class InitialLoadingScreen_Patch
    {
        public static GameObject gameObject;
        private static AssetBundle assetBundle;
        private static GameObject canvas;

        [HarmonyPostfix]
        internal static void Postfix()
        {
            if (assetBundle == null)
                assetBundle = AssetBundle.LoadFromMemory(Properties.Resources.nkhook_logo);
            if (canvas == null)
                canvas = assetBundle.LoadAsset("Canvas").Cast<GameObject>();

            gameObject = GameObject.Instantiate(canvas);
        }
    }

    [HarmonyPatch(typeof(InitialLoadingScreen), "StartCloseAnimation")]
    internal class LoadingScreen_Patch
    {
        [HarmonyPostfix]
        internal static void Postfix()
        {
            InitialLoadingScreen_Patch.gameObject.SetActive(false);
            GameObject.Destroy(InitialLoadingScreen_Patch.gameObject);
        }
    }
}
