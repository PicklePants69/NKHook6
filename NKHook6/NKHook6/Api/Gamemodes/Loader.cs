using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace NKHook6.Api.Gamemodes
{
    public class Loader
    {
        internal static GameObject canvas;
        internal static GameObject insantiatedCanvas;
        internal static AssetBundle assetBundle;
        
        internal static Image gamemodeUI;
        internal static Image scrollArea;
        internal static RectTransform modItemContainer;

        public static List<Gamemode> customGameModes = new List<Gamemode>();
        private static Loader loader;
        public static void Start()
        {
            if (loader == null)
                loader = new Loader();

            loader.Setup();

            Logger.Log("There are currently " + customGameModes.Count + " custom gamemodes");

            if (customGameModes.Count > 0)
            {
                customGameModes[0].isLoaded = true;
                customGameModes[0].AddModItem(0);
            }

            
        }

        private void Setup()
        {
            if (assetBundle == null)
                assetBundle = AssetBundle.LoadFromMemory(Properties.Resources.gamemode_ui);
            if (canvas == null)
                canvas = assetBundle.LoadAsset("Canvas").Cast<GameObject>();

            if (insantiatedCanvas == null)
                insantiatedCanvas = GameObject.Instantiate(canvas);

            if (gamemodeUI == null)
                gamemodeUI = insantiatedCanvas.transform.Find("Gamemode UI").GetComponent<Image>();

            if (scrollArea == null)
                scrollArea = gamemodeUI.transform.Find("ModItem Scroll Area").GetComponent<Image>();

            if (modItemContainer == null)
                modItemContainer = gamemodeUI.transform.Find("ModItem Scroll Area/ModItem Container").GetComponent<RectTransform>();
        }
    }
}