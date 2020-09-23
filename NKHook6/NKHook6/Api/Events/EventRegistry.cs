using NKHook6.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Events
{
    public class EventRegistry
    {
        public static EventRegistry subscriber;
        internal EventRegistry()
        {
            subscriber = this;

            theRegistry.Add("UpdateEvent", new List<MethodInfo>());
            theRegistry.Add("KeyPressEvent", new List<MethodInfo>());
            theRegistry.Add("KeyHeldEvent", new List<MethodInfo>());
            theRegistry.Add("KeyReleaseEvent", new List<MethodInfo>());
            theRegistry.Add("MainMenuShownEvent.Pre", new List<MethodInfo>());
            theRegistry.Add("MainMenuShownEvent.Post", new List<MethodInfo>());
        }

        /// <summary>
        /// Dictionary of eventNames with their callbacks
        /// </summary>
        Dictionary<string, List<MethodInfo>> theRegistry = new Dictionary<string, List<MethodInfo>>();

        public void register(Type toSubscribe)
        {
            foreach(MethodInfo method in toSubscribe.GetMethods())
            {
                if (method.IsStatic)
                {
                    foreach (Attribute attrib in method.GetCustomAttributes())
                    {
                        if(attrib is EventAttribute)
                        {
                            EventAttribute eventAttrib = (EventAttribute)attrib;
                            foreach(string currentEventName in theRegistry.Keys)
                            {
                                if (currentEventName == eventAttrib.eventName)
                                {
                                    theRegistry[currentEventName].Add(method);
                                    Logger.Log("Registered event \"" + eventAttrib.eventName + "\"");
                                    return;
                                }
                            }
                            Logger.Log("Unknown event \"" + eventAttrib.eventName + "\"");
                        }
                    }
                }
            }
        }
        public void unregister(Type toUnSubscribe)
        {

        }
        public void dispatchEvent<T>(ref T e) where T : EventBase
        {
            foreach (string name in theRegistry.Keys)
            {
                List<MethodInfo> callbacks = theRegistry[name];
                if (callbacks == null)
                    continue;
                if (callbacks.Count == 0)
                    continue;
                foreach(MethodInfo callback in callbacks)
                {
                    foreach (Attribute attrib in callback.GetCustomAttributes())
                    {
                        if (attrib is EventAttribute)
                        {
                            EventAttribute eventAttrib = (EventAttribute)attrib;
                            if (eventAttrib.eventName == e.eventName)
                            {
                                callback.Invoke(null, new object[] { e });
                            }
                        }
                    }
                }
            }
        }
    }
}
