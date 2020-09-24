using System;
using System.Collections.Generic;
using System.Reflection;

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


            string preName = ".Pre";
            string postName = ".Post";
            List<string> HarmonyEvents = new List<string>()
            {
                "MainMenuShownEvent",
                "BloonSpawnedEvent",
                "BloonDamagedEvent",
                "BloonLeakedEvent",
                "BloonPoppedEvent",
                "BloonSetRotationEvent",
                "BloonUpdatedModelEvent",

                "TowerUpdatedModelEvent",
                "TowerDestroyedEvent",
                "TowerSoldEvent",
                "TowerInitializedEvent",
                "TowerAddPoppedCashEvent",
                "TowerGetUpgradeEvent",

                "InGameUpdateEvent",
                "RoundStartEvent",
                "RoundEndEvent",               
            };

            foreach (var item in HarmonyEvents)
            {
                if (theRegistry.ContainsKey(item + preName) && theRegistry.ContainsKey(item + postName))
                    continue;

                theRegistry.Add(item + preName, new List<MethodInfo>());
                theRegistry.Add(item + postName, new List<MethodInfo>());
            }
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
