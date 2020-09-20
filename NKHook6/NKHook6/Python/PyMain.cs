using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.NKPython
{
    class PyMain
    {
        private static ScriptEngine pyEngine = null;
        private static ScriptRuntime pyRuntime = null;
        private static ScriptScope pyScope = null;

        public static void Setup()
        {
            //Python shit
            pyEngine = Python.CreateEngine();
            pyScope = pyEngine.CreateScope();
            pyScope.SetVariable("logger", Logger.instance);
        }
        public static void Execute(string script)
        {
            try
            {
                ScriptSource source = pyEngine.CreateScriptSourceFromString(script, SourceCodeKind.Statements);
                CompiledCode compiled = source.Compile();
                // Executes in the scope of Python
                compiled.Execute(pyScope);
            }catch(Exception ex)
            {
                Logger.instance.Log("Exception occoured when executing python code!");
                Logger.instance.Log(ex.Message);
                Logger.instance.Log(ex.StackTrace);
            }
        }
    }
}
