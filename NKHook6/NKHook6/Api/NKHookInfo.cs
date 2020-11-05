using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api
{
    public class NKHookInfo : MelonInfoAttribute
    {
        public string updateLink;
        public NKHookInfo(Type type, string name, string version, string author, string downloadLink = null, string updateLink = null) : base(type, name, version, author, downloadLink)
        {
            this.updateLink = updateLink;
        }
    }
}
