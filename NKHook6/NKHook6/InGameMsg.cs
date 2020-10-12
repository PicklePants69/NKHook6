using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Cascade.Variable;
using Assets.Scripts.Unity.UI_New.InGame;
using NKHook6.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using UnityEngine;
using UnityEngine.UI;

namespace NKHook6
{
    public enum Transition
    {
        Fade,
        Slide
    }

    public class NkhMsg
    {
        public NkhText NkhText;
        public Transition Transition = Transition.Slide;
        public float MsgShowTime = 1.5f;
        public NkhImage NkhImage = null;
    }

    public class NkhImage
    {
        public Image image;
        public Vector2 Position;
        public Vector2 Size;
    }

    public class NkhText
    {
        public string Title;
        public Color TitleColor = Color.white;
        public string Body;
        public Color BodyColor = Color.white;
        public Vector2? BodyPosition;
        public Vector2? BodySize;
    }


    internal class InGameMsg
    {
        internal static GameObject ingameMsgPopup;
        private static InGameMsg instance;
        private static Queue<NkhMsg> msgQueue = new Queue<NkhMsg>();

        private static bool doSetup = true;
        private static bool doShowMsg = false;
        private static bool doStallMsg = false;
        private static bool doHideMsg = false;
        private static readonly float defaultAlpha = 1f;

        internal static void QueueMsg(NkhMsg msg)
        {
            if (Game.instance == null || InGame.instance == null) //Have to do this for now because sending messages when not in game breaks mod
                return;

            if (ingameMsgPopup == null)
            {
                var assetBundle = AssetBundle.LoadFromMemory(Properties.Resources.ingame_popup);
                var go = assetBundle.LoadAsset("Canvas").Cast<GameObject>();

                ingameMsgPopup = GameObject.Instantiate(go);
                ingameMsgPopup.SetActive(false);
            }

            if (string.IsNullOrEmpty(msg.NkhText.Title))
            {
                if (Utils.GetCallingModInfo() == null)
                    msg.NkhText.Title = "NKHook6";
                else
                    msg.NkhText.Title = Utils.GetCallingModInfo().Name;
            }

            msgQueue.Enqueue(msg);
        }


        float next = 0f;
        internal static void Update()
        {
            if (ingameMsgPopup == null || msgQueue.Count == 0)
                return;
            
            if (!ingameMsgPopup.active)
                ingameMsgPopup.SetActive(true);

            if (instance == null)
                instance = new InGameMsg();

            if (doSetup)
                instance.MsgSetup();

            //This is used for testing purposes to see how the msgPopup moves over time within the game
            /*if (Time.time > instance.next)
            {
                var pos = instance.img.transform.position;
                instance.img.transform.position = new Vector3(pos.x, pos.y, pos.z + 6);
                Logger.Log((pos.z - 6).ToString());
                instance.next = Time.time + 0.01f;
            }*/

            if (doShowMsg)
                instance.ShowMsg();

            if (doStallMsg)
                instance.StallMsg();

            if (doHideMsg)
                instance.HideMsg();
        }




        private Image img;
        private Text title;
        private Text body;
        private NkhMsg currentMsg;

        private float nextAlpha = 0f;
        private float nextMsgRunTime = 0f;
        private readonly float waitTimePerRun = 0.05f;
        private readonly float transitionWaitTimePerRun = 0f;
        private static Vector3? bodyOrigin = null;

        private float nextX = 0;
        private readonly int defaultWidth = 345;
        private readonly int maxX = 20;
        private float defaultY = -99999;
        private void MsgSetup()
        {
            currentMsg = msgQueue.Peek();
            Logger.Log(currentMsg.NkhText.Body);

            img = ingameMsgPopup.transform.Find("Image").GetComponent<Image>();
            title = ingameMsgPopup.transform.Find("Image/Title").GetComponent<Text>();
            body = ingameMsgPopup.transform.Find("Image/Body").GetComponent<Text>();

            

            title.text = currentMsg.NkhText.Title;
            title.color = currentMsg.NkhText.TitleColor;

            body.text = currentMsg.NkhText.Body;
            body.color = currentMsg.NkhText.BodyColor;



            if (bodyOrigin == null)
                bodyOrigin = body.transform.position;

            /*if (currentMsg.NkhText.BodyPosition != null)
            {
                var pos = currentMsg.NkhText.BodyPosition;
                float x = pos.Value.x;
                float y = pos.Value.y;

                body.transform.position = new Vector3(bodyOrigin.Value.x + x, bodyOrigin.Value.y - y, bodyOrigin.Value.z);
            }*/


            if (currentMsg.Transition == Transition.Fade)
                SetAlpha(0);
            else if (currentMsg.Transition == Transition.Slide)
            {
                if (defaultY == -99999)
                    defaultY= img.transform.position.y - 35;

                nextX = -defaultWidth;
                SetAlpha(1);
                //Slide(-defaultWidth, defaultY);
            }

            doShowMsg = true;
            doStallMsg = false;
            doHideMsg = false;
            doSetup = false;
        }


        private void ShowMsg()
        {
            if (Time.time < nextMsgRunTime)
                return;

            if (currentMsg.Transition == Transition.Fade)
            {
                float amtToAdd = 0.08f;
                if ((img.color.a + amtToAdd) >= defaultAlpha)
                {
                    SetAlpha(defaultAlpha);
                    doShowMsg = false;
                    nextMsgRunTime = Time.time + currentMsg.MsgShowTime;
                    doStallMsg = true;
                    return;
                }

                nextAlpha += amtToAdd;
                SetAlpha(nextAlpha);
                nextMsgRunTime = Time.time + waitTimePerRun;
            }
            else if (currentMsg.Transition == Transition.Slide)
            {
                float amtToAdd = 10.5f;
                if ((img.transform.position.x + amtToAdd) >= maxX)
                {
                    Slide(maxX);
                    doShowMsg = false;
                    nextMsgRunTime = Time.time + currentMsg.MsgShowTime;
                    doStallMsg = true;
                    return;
                }

                nextX += amtToAdd;
                Slide(nextX);
                nextMsgRunTime = Time.time + transitionWaitTimePerRun;
            }
        }


        private void StallMsg()
        {
            if (Time.time < nextMsgRunTime)
                return;

            if (currentMsg.Transition == Transition.Fade)
                nextAlpha = img.color.a;

            doStallMsg = false;
            doHideMsg = true;
        }


        private void HideMsg()
        {
            if (Time.time < nextMsgRunTime)
                return;

            if (currentMsg.Transition == Transition.Fade)
            {
                float amtToSubtract = 0.08f;
                if (img.color.a - amtToSubtract <= 0)
                {
                    SetAlpha(0);
                    MsgCleanup();
                }

                nextAlpha -= amtToSubtract;
                SetAlpha(nextAlpha);
                nextMsgRunTime = Time.time + waitTimePerRun;
            }
            else if (currentMsg.Transition == Transition.Slide)
            {
                float amtToSubtract = 6.5f;
                if (img.transform.position.x - amtToSubtract <= -defaultWidth)
                {
                    Slide(-defaultWidth);
                    MsgCleanup();
                }

                nextX -= amtToSubtract;
                Slide(nextX);
                nextMsgRunTime = Time.time + transitionWaitTimePerRun;
            }
        }

        
        private void MsgCleanup()
        {
            doSetup = true;
            doShowMsg = false;
            doStallMsg = false;
            doHideMsg = false;
            
            msgQueue.Dequeue();
        }


        public void SetAlpha(float alpha)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            title.color = new Color(title.color.r, title.color.g, title.color.b, alpha);
            body.color = new Color(body.color.r, body.color.g, body.color.b, alpha);
        }


        public void Slide([Optional]float? x, [Optional]float? y, [Optional]float? z)
        {
            if (x == null && y == null)
                return;

            float testZ = 0;
            if (z != null)
                testZ = z.Value;

            if (x != null && y != null)
            {
                img.transform.position = new Vector3(x.Value, y.Value, img.transform.position.z);
            }
            else if (x != null && y == null)
            {
                img.transform.position = new Vector3(x.Value, img.transform.position.y, img.transform.position.z + testZ);
            }
            else if (x == null && y != null)
            {
                img.transform.position = new Vector3(img.transform.position.x, y.Value, img.transform.position.z);
            }
        }
    }
}
