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

            theRegistry.Add(new UpdateEvent(), new List<MethodInfo>());
        }

        Dictionary<EventBase, List<MethodInfo>> theRegistry = new Dictionary<EventBase, List<MethodInfo>>();

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
                            foreach(EventBase eb in theRegistry.Keys)
                            {
                                string currentEventName = eb.eventName;
                                if(currentEventName == eventAttrib.eventName)
                                {
                                    theRegistry[eb].Add(method);
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
            foreach (EventBase eb in theRegistry.Keys)
            {
                List<MethodInfo> callbacks = theRegistry[eb];
                if (callbacks == null)
                    return;
                if (callbacks.Count == 0)
                    return;
                foreach(MethodInfo callback in callbacks)
                {
                    callback.Invoke(null, new object[] { e });
                }
            }
        }
    }
}
