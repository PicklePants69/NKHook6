using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Extensions
{
    public static class ThreadingExt
    {
        public static void Raise<T>(this EventHandler<T> handler,
              object sender, T args) where T : EventArgs
        {
            if (handler != null) handler(sender, args);
        }
    }
}
