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
using static NKHook6.Logger;
using NKHook6.Scripting;

namespace NKHook6
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            Log("NKHook6 is initializing...");
            Log("CWD: " + Environment.CurrentDirectory);

            InitializePython();
            Log("NKHook6 initialized");
            InitializeCommandMgr();
        }

        private void InitializePython()
        {
            Log("Setting up python...");
            PyManager.Setup();
            Log("Python set up!");

            Log("Running test script...");
            if (PyManager.Execute(@"logger.Log('Hello from Python!');"))
            {
                Log("Test success!");
            }
            else
            {
                Log("Test failed!");
                return;
            }

            Log("Running all scripts...");
            PyManager.ExecuteAllScripts();
            Log("Scripts executed!");
        }

        private void InitializeCommandMgr()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Console.Write("x>");
                    string input = Console.ReadLine();
                    if (CommandManager.Instance.OnCommand != null)
                        CommandManager.Instance.OnCommand.Invoke(null, input);
                }
            }).Start();
        }
    }
}
