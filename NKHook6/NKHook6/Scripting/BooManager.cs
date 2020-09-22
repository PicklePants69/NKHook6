using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NKHook6.Scripting
{
    //👻 boo
    class BooManager
    {
        static string scriptPath = "Mods";
        private static List<Thread> scriptThreads = new List<Thread>();

        /// <summary>
        /// Executes all boo scripts
        /// </summary>
        public static void ExecuteAllScripts()
        {
            if (!Directory.Exists(scriptPath))
                Directory.CreateDirectory(scriptPath);

            string[] files = Directory.GetFiles(scriptPath);
            Logger.Log("Script count: " + files.Length);
            foreach (string file in files)
            {
                if (!file.EndsWith(".boo"))
                    continue;
                Logger.Instance.Log("Loading script: " + file);

                scriptThreads.Add(new Thread(() =>
                {
                    if (!ExecuteFile(file))
                    {
                        Logger.Log("Failed to load script " + file);
                    }
                }));
                scriptThreads[scriptThreads.Count - 1].Start();
            }
        }
        /// <summary>
        /// Executes a boo script from a file
        /// </summary>
        /// <param name="path">Path to the script</param>
        /// <returns></returns>
        public static bool ExecuteFile(string path)
        {
            string name = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Path.GetFileNameWithoutExtension(path));
            Logger.Log("Name: " + name);
            string code = File.ReadAllText(path);
            return Execute(name, code);
        }

        /// <summary>
        /// Compiles and executed given .boo code
        /// </summary>
        /// <param name="name">Name of the script (Same as file name if reading from a file)</param>
        /// <param name="code">Code of the script</param>
        /// <returns></returns>
        public static bool Execute(string name, string code)
        {
            try
            {
                var compiler = new BooCompiler();
                compiler.Parameters.Input.Add(new StringInput(name, code));
                compiler.Parameters.Pipeline = new CompileToMemory();
                compiler.Parameters.Ducky = true;

                CompilerContext context = compiler.Run();
                if (context.GeneratedAssembly == null)
                {
                    foreach (CompilerError error in context.Errors)
                        Logger.Instance.Log(error.ToString());
                    return false;
                }
                Type entryModule = context.GeneratedAssembly.GetType(name + "Module");
                MethodInfo entryMethod = entryModule.GetMethod("Entry");
                return (bool)entryMethod.Invoke(null, null);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
                return false;
            }
            //return true;
        }
    }
}
