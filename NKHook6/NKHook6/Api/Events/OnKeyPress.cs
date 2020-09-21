using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NKHook6.Api.Events
{
    public class OnKeyPress
    {
        public static event EventHandler<KeyPressEventArgs> KeyPress;

        public class KeyPressEventArgs : EventArgs
        {
            public KeyCode key;
            public KeyPressEventArgs(KeyCode key)
            {
                this.key = key;
            }
        }

        internal static void InvokeOnKeyPressEvent()
        {
            EventHandler<KeyPressEventArgs> handler = KeyPress;
            if (handler != null)
                handler(null, null);
        }

        private static Dictionary<KeyCode, bool> wasDown = new Dictionary<KeyCode, bool>();
        private static void update(object sender, EventArgs none)
        {
            if (KeyPress == null)
                return;
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                    if (!wasDown[key])
                        KeyPress.Invoke(null, new KeyPressEventArgs(key));
            }
        }

        internal static void setupEvent()
        {
            OnUpdate.UpdateEvent += update;
            foreach(KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if(!wasDown.ContainsKey(key))
                    wasDown.Add(key, Input.GetKeyDown(key));
            }
        }
    }
}
