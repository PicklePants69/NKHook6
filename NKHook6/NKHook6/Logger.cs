using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Display.Animation;
using Assets.Scripts.Unity.UI_New.InGame;
using NKHook6.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using Utils = NKHook6.Api.Utilities.Utils;

namespace NKHook6
{
    public class Logger
    {
        public enum Level
        {
            Normal,
            Warning,
            Error
        }

        public static void Log(string text) => Log(text, Level.Normal, "");

        public static void Log(string text, Level level = Level.Normal, string sender = "") =>
            Log(text, (int)ConsoleColor.Red, level, sender);

        public static void Log(string text, int color = (int)ConsoleColor.Red, Level level = Level.Normal, string sender = "")
        {
            string modName = (Utils.GetCallingModInfo() == null ? "NKHook6" : Utils.GetCallingModInfo().Name);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            String time = DateTime.Now.ToString("HH:mm:ss.fff");
            Console.Write(time);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("] ");
            Console.ResetColor();
            Console.Write("[");
            Console.ForegroundColor = (ConsoleColor)color;
            Console.Write(modName);
            Console.ResetColor();
            Console.Write("] ");
            Console.WriteLine(text);
        }

        public static void ShowInGamePopup()
        {
            var a = new InGame();
            a.ShowEventPopup("AAAAA", 1);
            //InGame.instance.ShowEventPopup("EventTest", 0);
            ///Assets.Scripts.Unity.UI_New.InGame.InGame.ShowEventPopup("TestEvent", 0);
        }
    }
}
