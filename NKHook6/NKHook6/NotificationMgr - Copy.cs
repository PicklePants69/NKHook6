using Assets.Scripts.Unity;
using Boo.Lang;
using System;
//using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace NKHook6
{
    public class Notification
    {
        /*public Timer startTimer;
        public Timer showTimer;
        public Timer stopTimer;

        private Timer startStepTimer;
        
        public GameObject gameObject;
        private static GameObject canvas;
        public TimerCallback ExpireCallback;

        public Image img;
        public Notification()
        {
            if (canvas == null)
            {
                var assetBundle = AssetBundle.LoadFromMemory(Properties.Resources.ingame_popup);
                canvas = assetBundle.LoadAsset("Canvas").Cast<GameObject>();
            }

            
            gameObject = GameObject.Instantiate(canvas);

            img = gameObject.transform.Find("Image").GetComponent<Image>();
            var pos = img.transform.position;

            pos.x = -345;
            img.transform.position = pos;

            startTimer = new Timer(new TimerCallback(FinishStartAnim), this, 200, Timeout.Infinite);
            startStepTimer = new Timer(new TimerCallback(StepStartAnim), this, 0, 10*2);
        }

        float maxX = 20;
        public void StepStartAnim(object obj)
        {
            lock (startStepTimer)
            {
                var pos = img.transform.position;

                if (pos.x <= maxX)
                {
                    pos.x += 18*2;
                    img.transform.position = pos;
                }
            }
        }

        public void FinishStartAnim(object obj)
        {
            lock (startStepTimer)
            {
                startStepTimer.Dispose();
            }
            
            startTimer.Dispose();
            


            showTimer = new Timer(new TimerCallback(FinishShowAnim), this, 2000, Timeout.Infinite);
        }

        public void FinishShowAnim(object obj)
        {
            showTimer.Dispose();

            stopTimer = new Timer(new TimerCallback(ExpireCallback), this, 200, Timeout.Infinite);
        }*/
    }


    static class NotificationMgr
    {
        /*static List<Notification> notifications = new List<Notification>();

        public static void AddNotification()
        {
            Notification notification = new Notification();
            notification.ExpireCallback = new TimerCallback(NotificationExpire);
            //notification.startTimer = new Timer(notification.ExpireCallback, notification, 2000, 0);

            lock (notifications)
            {
                notifications.Add(notification);
            }
            
        }

        public static void NotificationExpire(object obj)
        {
            var notif = (Notification)obj;

            notif.stopTimer.Dispose();

            notif.gameObject.SetActive(false);
            GameObject.Destroy(notif.gameObject);

            lock (notifications)
            {
                notifications.Remove((Notification)obj);
            }
        }*/
    }
}