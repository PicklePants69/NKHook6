using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static NKHook6.Logger;

namespace NKHook6.Api.Utilities
{
    /// <summary>
    /// An assembly patcher for BTD6. Thanks to Jambyte for making this
    /// </summary>
    public unsafe class ASMPatcher
    {
        const int PROCESS_WM_READ = 0x0010;//constants for kernal32 stuff
        const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(int hProcess, Int64 lpBaseAddress, byte[] buffer, int size, ref int lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);
        Process btd6process;//cache the process
        ProcessModule gameassembly_pm;//cache the process moudle
        IntPtr processHandle;
        public ASMPatcher()
        {
            btd6process = Process.GetProcessesByName("BloonsTD6")[0];
            int i = 0;
            foreach (ProcessModule processModule in btd6process.Modules)
            {
                Console.WriteLine(processModule.FileName);
                if (processModule.FileName.Contains("GameAssembly.dll"))
                {
                    gameassembly_pm = btd6process.Modules[i];
                    break;
                }
                i++;
            }
            processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, btd6process.Id);
        }
        //named like this to stop c# keywords
        public bool Patch(string assembly, string namespaze, string clazz, string method, string returntype, bool isgeneric, string[] args, int offset, byte[] patchbytes)
        {
            IntPtr MethodClass_p = UnhollowerBaseLib.IL2CPP.GetIl2CppClass(assembly, namespaze, clazz);
            if (MethodClass_p != IntPtr.Zero)
            {
                Log("Found Class " + clazz + " in " + namespaze);
                return Patch(MethodClass_p, method, isgeneric, returntype, args, offset, patchbytes);
            }
            Log("Failed to find Class " + clazz + " in " + namespaze);
            return false;
        }
        public bool Patch(IntPtr clazz, string method, bool isgeneric, string returntype, string[] args, int offset, byte[] patchbytes)
        {
            IntPtr Method_p = UnhollowerBaseLib.IL2CPP.GetIl2CppMethod(clazz, isgeneric, method, returntype, args);
            if (Method_p != IntPtr.Zero)
            {
                Log("Found Method " + method + " in " + clazz);
                return Patch(Method_p, offset, patchbytes);
            }
            Log("Failed to find Method " + method + " in " + clazz);
            return false;
        }
        public bool Patch(IntPtr method, int offset, byte[] patchbytes)
        {
            long pointer = *((long*)method);
            //in il2cpp the methodinfo first field is a pointer to the method, used a lot in btd6api 
            pointer += offset;
            return Patch(pointer, patchbytes);
        }
        public bool PatchOffset(IntPtr offset, byte[] patchbytes)
        {
            return Patch(gameassembly_pm.BaseAddress.ToInt64() + offset.ToInt64(), patchbytes);
        }

        public bool Patch(long pointer, byte[] patchbytes)
        {
            int bytesWritten = 0;
            bool success = (WriteProcessMemory((int)processHandle, pointer, patchbytes, patchbytes.Length, ref bytesWritten));
            if (success)
            {
                Logger.Log("Wrote " + bytesWritten + " bytes at " + pointer);

            }
            else
            {
                Logger.Log("Failed to Write bytes at " + pointer);
            }
            return success;
        }



        public bool ReadMemory(string assembly, string namespaze, string clazz, string method, string returntype, bool isgeneric, string[] args, int offset, byte[] readbytes)
        {
            IntPtr MethodClass_p = UnhollowerBaseLib.IL2CPP.GetIl2CppClass(assembly, namespaze, clazz);
            if (MethodClass_p != IntPtr.Zero)
            {
                Log("Found Class " + clazz + " in " + namespaze);
                return Patch(MethodClass_p, method, isgeneric, returntype, args, offset, readbytes);
            }
            Log("Failed to find Class " + clazz + " in " + namespaze);
            return false;
        }
        public bool ReadMemory(IntPtr clazz, string method, bool isgeneric, string returntype, string[] args, int offset, byte[] readbytes)
        {
            IntPtr Method_p = UnhollowerBaseLib.IL2CPP.GetIl2CppMethod(clazz, isgeneric, method, returntype, args);
            if (Method_p != IntPtr.Zero)
            {
                Log("Found Method " + method + " in " + clazz);
                return Patch(Method_p, offset, readbytes);
            }
            Log("Failed to find Method " + method + " in " + clazz);
            return false;
        }
        public bool ReadMemory(IntPtr method, int offset, byte[] readbytes)
        {
            long pointer = *((long*)method);
            //in il2cpp the methodinfo first field is a pointer to the method, used a lot in btd6api 
            pointer += offset;
            return Patch(pointer, readbytes);
        }
        public bool ReadMemoryOffset(IntPtr offset, byte[] patchbytes)
        {
            return Patch(gameassembly_pm.BaseAddress.ToInt64() + offset.ToInt64(), patchbytes);
        }

        public bool ReadMemory(long pointer, byte[] readbytes)
        {
            int bytesWritten = 0;
            bool success = (ReadProcessMemory((int)processHandle, pointer, readbytes, readbytes.Length, ref bytesWritten));
            if (success)
            {
                Logger.Log("Read " + bytesWritten + " bytes at " + pointer);

            }
            else
            {
                Logger.Log("Failed to Read bytes at " + pointer);
            }
            return success;
        }
    }
}

//example patch for glaive god
/*
ASMPatcher asm = new ASMPatcher();
byte[] patch = new byte[] { 0x66, 0x83, 0xc6, 0x20, 0x90, 0x0f, 0x8d };//patch 3 asm to add, nop, jge
asm.Patch("Assembly-CSharp.dll", "Assets.Scripts.Simulation.Towers.Behaviors", "Orbit", "Attatched", "System.Void", false, new string[] { }, 0x20A, patch);
*/


//IntPtr MethodClass_p = UnhollowerBaseLib.IL2CPP.GetIl2CppClass("Assembly-CSharp.dll", "Assets.Scripts.Simulation.Towers.Behaviors", "Orbit");
//Log(MethodClass_p.ToString());
//IntPtr Method_p = UnhollowerBaseLib.IL2CPP.GetIl2CppMethod(MethodClass_p, false, "Attatched", "System.Void", new string[] { });
//Log(Method_p.ToString());