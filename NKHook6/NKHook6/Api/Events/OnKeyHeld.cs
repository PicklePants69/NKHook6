using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NKHook6.Api.Events
{
    public class OnKeyHeld
    {
        public static event EventHandler<KeyHeldEventArgs> KeyHeld;

        public class KeyHeldEventArgs : EventArgs
        {
            public KeyCode key;
            public KeyHeldEventArgs(KeyCode key)
            {
                this.key = key;
            }
        }

        internal static void InvokeOnKeyHeldEvent(KeyCode key)
        {
            EventHandler<KeyHeldEventArgs> handler = KeyHeld;
            if (handler != null)
                handler(null, new KeyHeldEventArgs(key));
        }

        private static void update(object sender, EventArgs none)
        {
            if (KeyHeld == null)
                return;
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(key))
                    InvokeOnKeyHeldEvent(key);
            }
        }

        internal static void setupEvent()
        {
            //OnUpdate.UpdateEvent += update;
        }
    }
}
