using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Unity;
using UnityEngine.Playables;
using System.IO;
using System.Threading;
using NKHook6.Api;
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

            logger.Log("Setting up python...");
            PyManager.Setup();
            logger.Log("Python set up!");
            string testScript = @"logger.Log('Hello from Python!');";
            logger.Log("Running test script...");
            if (PyManager.Execute(testScript))
            {
                logger.Log("Test success!");
            }
            else
            {
                logger.Log("Test failed!");
                return;
            }

            new CommandManager();

            logger.Log("Running all scripts...");
            PyManager.ExecuteAllScripts();
            logger.Log("Scripts executed!");

            logger.Log("NKHook6 initialized");

            new Thread(() =>
            {
                while (true)
                {
                    Console.Write("x>");
                    string input = Console.ReadLine();
                    if(CommandManager.instance.OnCommand != null)
                        CommandManager.instance.OnCommand.Invoke(null, input);
                }
            }).Start();
        }
    }
}
