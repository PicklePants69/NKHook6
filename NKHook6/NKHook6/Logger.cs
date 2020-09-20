using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Sender { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().FullName;

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


        public void Log(string text, int color = (int)ConsoleColor.Red, Level level = Level.Normal) => Log(text, color, level, Sender);

        public static void Log(string text, int color = (int)ConsoleColor.Red, Level level = Level.Normal, string sender = "")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("(" + DateTime.Now + ") ");
            Console.ResetColor();
            Console.Write("[");
            Console.ForegroundColor = (ConsoleColor)color;
            Console.Write(sender);
            Console.ResetColor();
            Console.Write("] ");
            Console.WriteLine(text);
        }
    }
}
