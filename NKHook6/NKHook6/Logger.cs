using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Display.Animation;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.UI_New.Popups;
using Il2CppSystem.Reflection;
using NKHook6.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            Error,
            UpdateNotify
        }

        public static void Log(string text) => Log(text, Level.Normal, "");

        public static void Log(string text, string sender) => Log(text, Level.Normal, sender);

        public static void Log(string text, Level level, string sender = "") =>
            Log(text, (int)ConsoleColor.Red, level, sender);

        public static void Log(string text, int color, Level level, string sender = "")
        {
            string modName;
            if (String.IsNullOrEmpty(sender))
                modName = (Utils.GetCallingModInfo() == null ? "NKHook6" : Utils.GetCallingModInfo().Name);
            else
                modName = sender;

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
            Console.ForegroundColor = GetLoggerColor(level);  //Change color of message to match logger level
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static ConsoleColor GetLoggerColor(Level level)
        {
            switch ((int)level)
            {
                case 1: //warning
                    return ConsoleColor.Yellow;
                case 2:  //error
                    return ConsoleColor.Red;
                case 3:
                    return ConsoleColor.Green;
                default:  //normal
                    return ConsoleColor.Gray;
            }
        }

        public static void ShowMsgPopup(string title, string body)
        {
            //PopupScreen.instance.(new Action(() => ));

            try
            {

                PopupScreen.instance.ShowEventPopup(PopupScreen.Placement.menuCenter, title, body, "Okay",
                 null, null, null, Popup.TransitionAnim.Scale, 0, PopupScreen.BackGround.None);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
        }

        internal static void ReturnCallbackTest()
        {

        }
    }
}
