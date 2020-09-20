using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6
{
    public class Logger
    {
        public static Logger instance;
        public Logger()
        {
            instance = this;
        }
        public void Log(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("(" + DateTime.Now + ") ");
            Console.ResetColor();
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("NKHook6");
            Console.ResetColor();
            Console.Write("] ");
            Console.WriteLine(text);
        }
    }
}
