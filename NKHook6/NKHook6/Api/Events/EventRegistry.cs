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

            createEvent("UpdateEvent"); //Updated
            createEvent("KeyPressEvent"); //Updated
            createEvent("KeyHeldEvent"); //Updated
            createEvent("KeyReleaseEvent"); //Updated

            createEvent("MainMenuLoadedEvent"); //Updated
            createEvent("BloonCreatedEvent"); //Updated
            createEvent("BloonDamagedEvent"); //Updated
            createEvent("BloonLeakedEvent"); //Updated
            createEvent("BloonDeletedEvent"); //Updated
            //createEvent("BloonMoveEvent"); //Not possible yet, gotta do more investigation
            createEvent("BloonRotateEvent"); //Updated
            //createEvent("BloonModelChangedEvent"); //Kinda useless

            createEvent("TowerCreatedEvent"); //Updated
            createEvent("TowerDeletedEvent"); //Updated
            createEvent("TowerSoldEvent"); //Updated
            //createEvent("TowerModelChangedEvent"); //Kinda useless
            createEvent("TowerSelectedEvent"); //Updated
            createEvent("TowerDeselectedEvent"); //Updated
            createEvent("TowerUpgradeEvent"); //Updated

            createEvent("RoundStartEvent"); //Updated
            createEvent("RoundEndEvent"); //Updated

            createEvent("WeaponCreatedEvent"); //Updated
            createEvent("WeaponDeletedEvent"); //Updated
            //createEvent("WeaponModelChangedEvent"); //Kinda useless

            createEvent("VictoryEvent");
            createEvent("DefeatedEvent");
            createEvent("HealthChangedEvent");
            createEvent("HealthLostEvent");
            createEvent("HealthGainedEvent");
            createEvent("CashChangedEvent");
            createEvent("CashLostEvent");
            createEvent("CashGainedEvent");

            createEvent("FastForwardToggleEvent");

            createEvent("ProjectileCreatedEvent");
            createEvent("ProjectileDeletedEvent");
            createEvent("ProjectileModelChangedEvent");
        }

        void createEvent(string eventName)
        {
            try
            {
                theRegistry.Add(eventName, new List<MethodInfo>());
                Logger.Log("Created event: " + eventName);
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to create event: " + eventName);
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
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
                            bool registered = false;
                            EventAttribute eventAttrib = (EventAttribute)attrib;
                            foreach(string currentEventName in theRegistry.Keys)
                            {
                                if (currentEventName == eventAttrib.eventName)
                                {
                                    theRegistry[currentEventName].Add(method);
                                    Logger.Log("Registered event \"" + eventAttrib.eventName + "\"");
                                    registered = true;
                                    continue;
                                }
                            }
                            if(!registered)
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
