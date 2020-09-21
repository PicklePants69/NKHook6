using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

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

        public string Sender { get; set; } = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;

        private static Logger instance;

        public static Logger Instance
        {
            get 
            {
                if (instance == null)
                    instance = new Logger();
                return instance; 
            }
            set { instance = value; }
        }


        public void Log(string text, int color = (int)ConsoleColor.Red, Level level = Level.Normal)
        {
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
            Console.Write(Sender);
            Console.ResetColor();
            Console.Write("] ");
            Console.WriteLine(text);
        }

        public static void Log(string text, Level level = Level.Normal, string sender = "") =>
            Log(text, (int)ConsoleColor.Red, level, sender);

        public static void Log(string text, int color = (int)ConsoleColor.Red, Level level = Level.Normal, string sender = "")
        {
            if (!String.IsNullOrEmpty(sender))
                Instance.Sender = sender;

            Instance.Log(text, color, level);
        }
    }
}
