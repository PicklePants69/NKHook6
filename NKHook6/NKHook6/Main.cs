using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKHook6.NKPython;

namespace NKHook6
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            Logger logger = new Logger();
            logger.Log("NKHook6 is initializing...");
            logger.Log("CWD: " + Environment.CurrentDirectory);


            string testScript = @"logger.Log('Hello from Python!');";
            PyMain.Execute(testScript);

            logger.Log("NKHook6 initialized");
        }
    }
}
