using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NKHook6.Api.Events
{
    public class OnKeyRelease
    {
        public static event EventHandler<KeyReleaseEventArgs> KeyRelease;

        public class KeyReleaseEventArgs : EventArgs
        {
            public KeyCode key;
            public KeyReleaseEventArgs(KeyCode key)
            {
                this.key = key;
            }
        }

        internal static void InvokeOnKeyReleaseEvent(KeyCode key)
        {
            EventHandler<KeyReleaseEventArgs> handler = KeyRelease;
            if (handler != null)
                handler(null, new KeyReleaseEventArgs(key));
        }

        private static void update(object sender, EventArgs none)
        {
            if (KeyRelease == null)
                return;
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyUp(key))
                    InvokeOnKeyReleaseEvent(key);
            }
        }

        internal static void setupEvent()
        {
            //OnUpdate.UpdateEvent += update;
        }
    }
}
