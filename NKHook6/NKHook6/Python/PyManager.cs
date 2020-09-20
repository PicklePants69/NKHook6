using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppSystem.Net;
using System.IO;
using System.Threading;

namespace NKHook6.NKPython
{
    class PyManager
    {
        private static ScriptEngine pyEngine = null;
        private static ScriptRuntime pyRuntime = null;
        private static ScriptScope pyScope = null;

        private static List<Thread> scriptThreads = new List<Thread>();

        public static void Setup()
        {
            //Python shit
            pyEngine = Python.CreateEngine();
            pyScope = pyEngine.CreateScope();
            pyScope.SetVariable("logger", Logger.Instance);
        }
        public static void ExecuteAllScripts()
        {
            if (!Directory.Exists("Scripts"))
                Directory.CreateDirectory("Scripts");

            string[] files = Directory.GetFiles("Scripts");
            foreach(string file in files)
            {
                if (file.EndsWith(".py"))
                {
                    Logger.Instance.Log("Loading script: " + file);
                    Thread execThread = new Thread(() =>
                    {
                        if (!ExecuteFile(file))
                        {
                            Logger.Instance.Log("Failed to load script " + file);
                        }
                    });
                    execThread.Start();
                    scriptThreads.Add(execThread);
                }
            }
        }
        public static bool ExecuteFile(string filename)
        {
            string contents = File.ReadAllText(filename);
            return Execute(contents);
        }
        public static bool Execute(string script)
        {
            try
            {
                /*
                 * Pre add all references for a python mod, as well as imports.
                 */
                string scriptHead = @"import clr;" + Environment.NewLine;
                string[] files = Directory.GetFiles("MelonLoader/Managed/");
                foreach (string file in files)
                {
                    string sanitized = file.Replace("MelonLoader/Managed/", "").Replace(".dll", "");
                    if (sanitized.EndsWith(".db"))
                    {
                        continue;
                    }
                    scriptHead += "clr.AddReference('" + sanitized + "');" + Environment.NewLine;
                }
                script = scriptHead + script;

                ScriptSource source = pyEngine.CreateScriptSourceFromString(script, SourceCodeKind.Statements);
                CompiledCode compiled = source.Compile();
                // Executes in the scope of Python
                compiled.Execute(pyScope);
                return true;
            }catch(Exception ex)
            {
                Logger.Instance.Log("Exception occoured when executing python code!", level: Logger.Level.Error);
                Logger.Instance.Log(ex.Message, level: Logger.Level.Error);
                Logger.Instance.Log(ex.StackTrace, level: Logger.Level.Error);
                return false;
            }
        }
    }
}
