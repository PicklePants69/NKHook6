using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Scripting
{
    //👻 boo
    public class BooManager
    {
        public static bool CompileAndExecute(string name, string code)
        {
            var compiler = new BooCompiler();
            compiler.Parameters.Input.Add(new StringInput(name, code));
            compiler.Parameters.Pipeline = new CompileToMemory();
            compiler.Parameters.Ducky = true;

            CompilerContext context = compiler.Run();
            if(context.GeneratedAssembly == null)
            {
                foreach (CompilerError error in context.Errors)
                    Logger.Instance.Log(error.ToString());
                return false;
            }


            return true;
        }
    }
}
