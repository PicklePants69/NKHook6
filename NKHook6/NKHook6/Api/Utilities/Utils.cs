using Il2CppSystem.Reflection;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Utilities
{
    public class Utils
    {
        /// <summary>
        /// Get the Assembly of the mod that is calling NKHook6 code
        /// </summary>
        /// <returns></returns>
        public static System.Reflection.Assembly GetCallingMod() => GetMod(1);

        /// <summary>
        /// Get Assembly of mod at index
        /// </summary>
        /// <param name="index">StackTrace index of the mod you want to get Asm for.</param>
        /// <returns></returns>
        public static System.Reflection.Assembly GetMod(int index)
        {
            StackFrame[] frames = new StackTrace().GetFrames();
            var asmNames = (from f in frames
                            select f.GetMethod().ReflectedType.Assembly
                                     ).Distinct().ToList();

            return asmNames[index];
        }

        /// <summary>
        /// Get the MelonInfo of the mod that is calling NKHook6 code
        /// </summary>
        /// <returns></returns>
        public static MelonInfoAttribute GetCallingModInfo() => GetModInfo(GetCallingMod());

        /// <summary>
        /// Get the MelonInfo for the mod located at "index" in the StackTrace
        /// </summary>
        /// <param name="index">Location of mod in StackTrace</param>
        /// <returns></returns>
        public static MelonInfoAttribute GetModInfo(int index) => GetModInfo(GetMod(index));

        /// <summary>
        /// Get the MelonInfo of the mod with the provided Assembly
        /// </summary>
        /// <param name="mod">Assembly of the mod you want to get MelonInfo for</param>
        /// <returns></returns>
        public static MelonInfoAttribute GetModInfo(System.Reflection.Assembly mod)
        {
            MelonInfoAttribute callingMod = null;

            var cust = MelonInfoAttribute.GetCustomAttributes(mod);
            foreach (var item in cust)
            {
                if (item is MelonInfoAttribute)
                    callingMod = (MelonInfoAttribute)item;
            }

            return callingMod;
        }
    }
}
