using Microsoft.ClearScript.V8;
using NKHook6.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Scripting
{
    class ClearScript
    {
        public static ClearScript instance;
        public ClearScript()
        {
            instance = this;
            var engine = new V8ScriptEngine();
            engine.AddHostType("Logger", typeof(Logger));
        }
    }
}
