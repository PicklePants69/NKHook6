using NKHook6.Api.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NKHook6
{
    class KeyListener
    {
        public KeyListener()
        {
            EventRegistry.instance.listen(this.GetType());
        }

        [EventAttribute("UpdateEvent")]
        public static void onUpdate(ref UpdateEvent updateEvent)
        {
            foreach(KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    KeyPressEvent keyEvent = new KeyPressEvent(key);
                    EventRegistry.instance.dispatchEvent(ref keyEvent);
                    continue;
                }
                if (Input.GetKey(key))
                {
                    KeyHeldEvent keyEvent = new KeyHeldEvent(key);
                    EventRegistry.instance.dispatchEvent(ref keyEvent);
                    continue;
                }
                if (Input.GetKeyUp(key))
                {
                    KeyReleaseEvent keyEvent = new KeyReleaseEvent(key);
                    EventRegistry.instance.dispatchEvent(ref keyEvent);
                    continue;
                }
            }
        }
    }
}
