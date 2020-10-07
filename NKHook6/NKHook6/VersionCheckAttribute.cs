using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.Main;
using Boo.Lang;
using NKHook6.Api.Events;
using NKHook6.Api.Events._MainMenu;
using NKHook6.Api.Web;
using System;
using System.Linq;
using System.Reflection;

namespace NKHook6
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class LatestVersionURLAttribute : Attribute
    {
        internal static List<LatestVersionURLAttribute> loaded;
        public string url;
        public Type type;
        public LatestVersionURLAttribute(Type type, string url) 
        {
            this.url = url;
            this.type = type;

            if (loaded == null)
                loaded = new List<LatestVersionURLAttribute>();

            if (!loaded.Contains(this))
                loaded.Add(this);
        }
    }
}
