using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Events
{
    public class OnUpdate
    {
        public static event EventHandler<EventArgs> UpdateEvent;

        internal static void InvokeOnUpdateEvent()
        {
            EventHandler<EventArgs> handler = UpdateEvent;
            if (handler != null)
                handler(null, null);
        }
    }
}
