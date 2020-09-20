using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppSystem.Net;
using System.IO;
using System.Threading;

namespace NKHook6.NKPython
{
    class PyManager
    {
        private static ScriptEngine pyEngine = null;
        private static ScriptRuntime pyRuntime = null;
        private static ScriptScope pyScope = null;

        private static List<Thread> scriptThreads = new List<Thread>();

        public static void Setup()
        {
            //Python shit
            pyEngine = Python.CreateEngine();
            pyScope = pyEngine.CreateScope();
            pyScope.SetVariable("logger", Logger.instance);
        }
        public static void ExecuteAllScripts()
        {
            string[] files = Directory.GetFiles("Scripts");
            foreach(string file in files)
            {
                if (file.EndsWith(".py"))
                {
                    Logger.instance.Log("Loading script: " + file);
                    Thread execThread = new Thread(() =>
                    {
                        if (!ExecuteFile(file))
                        {
                            Logger.instance.Log("Failed to load script " + file);
                        }
                    });
                    execThread.Start();
                    scriptThreads.Add(execThread);
                }
            }
        }
        public static bool ExecuteFile(string filename)
        {
            string contents = File.ReadAllText(filename);
            return Execute(contents);
        }
        public static bool Execute(string script)
        {
            try
            {
                /*
                 * Pre add all references for a python mod, as well as imports.
                 */
                #region clr-references
                script = @"
import clr;
clr.AddReference('Accessibility');
clr.AddReference('Assembly-CSharp-firstpass');
clr.AddReference('Assembly-CSharp');
clr.AddReference('Boo.Lang.Compiler');
clr.AddReference('Boo.Lang');
clr.AddReference('Boo.Lang.Extensions');
clr.AddReference('Boo.Lang.Parser');
clr.AddReference('Boo.Lang.PatternMatching');
clr.AddReference('Boo.Lang.Useful');
clr.AddReference('Commons.Xml.Relaxng');
clr.AddReference('cscompmgd');
clr.AddReference('CustomMarshalers');
clr.AddReference('Facebook.Unity.Settings');
clr.AddReference('Facepunch.Steamworks');
clr.AddReference('Firebase.Analytics');
clr.AddReference('Firebase.App');
clr.AddReference('Firebase.Crashlytics');
clr.AddReference('Firebase.Platform');
clr.AddReference('I18N.CJK');
clr.AddReference('I18N');
clr.AddReference('I18N.MidEast');
clr.AddReference('I18N.Other');
clr.AddReference('I18N.Rare');
clr.AddReference('I18N.West');
clr.AddReference('IBM.Data.DB2');
clr.AddReference('Iced');
clr.AddReference('ICSharpCode.SharpZipLib');
clr.AddReference('Il2CppMono.Security');
clr.AddReference('Il2Cppmscorlib');
clr.AddReference('Il2CppSystem.Configuration');
clr.AddReference('Il2CppSystem.Core');
clr.AddReference('Il2CppSystem.Diagnostics.StackTrace');
clr.AddReference('Il2CppSystem');
clr.AddReference('Il2CppSystem.Globalization.Extensions');
clr.AddReference('Il2CppSystem.IO.Compression');
clr.AddReference('Il2CppSystem.Runtime.Serialization');
clr.AddReference('Il2CppSystem.Xml');
clr.AddReference('Il2CppSystem.Xml.Linq');
clr.AddReference('Microsoft.CSharp');
clr.AddReference('Microsoft.Web.Infrastructure');
clr.AddReference('Mono.CompilerServices.SymbolWriter');
clr.AddReference('Mono.CSharp');
clr.AddReference('Mono.Data.Sqlite');
clr.AddReference('Mono.Data.Tds');
clr.AddReference('Mono.Messaging');
clr.AddReference('Mono.Posix');
clr.AddReference('Mono.Security');
clr.AddReference('Mono.WebBrowser');
clr.AddReference('mscorlib');
clr.AddReference('NCalc');
clr.AddReference('netstandard');
clr.AddReference('Newtonsoft.Json');
clr.AddReference('NinjaKiwi.Common');
clr.AddReference('Ninjakiwi.LiNK');
clr.AddReference('NK-Unity-Libs');
clr.AddReference('Novell.Directory.Ldap');
clr.AddReference('Purchasing.Common');
clr.AddReference('SMDiagnostics');
clr.AddReference('Stores');
clr.AddReference('System.ComponentModel.Composition');
clr.AddReference('System.ComponentModel.DataAnnotations');
clr.AddReference('System.Configuration');
clr.AddReference('System.Configuration.Install');
clr.AddReference('System.Core');
clr.AddReference('System.Data.DataSetExtensions');
clr.AddReference('System.Data');
clr.AddReference('System.Data.Entity');
clr.AddReference('System.Data.Linq');
clr.AddReference('System.Data.OracleClient');
clr.AddReference('System.Data.Services.Client');
clr.AddReference('System.Data.Services');
clr.AddReference('System.Design');
clr.AddReference('System.DirectoryServices');
clr.AddReference('System.DirectoryServices.Protocols');
clr.AddReference('System');
clr.AddReference('System.Drawing.Design');
clr.AddReference('System.Drawing');
clr.AddReference('System.EnterpriseServices');
clr.AddReference('System.IdentityModel');
clr.AddReference('System.IdentityModel.Selectors');
clr.AddReference('System.IO.Compression');
clr.AddReference('System.IO.Compression.FileSystem');
clr.AddReference('System.Json');
clr.AddReference('System.Management');
clr.AddReference('System.Messaging');
clr.AddReference('System.Net');
clr.AddReference('System.Net.Http');
clr.AddReference('System.Net.Http.Formatting');
clr.AddReference('System.Net.Http.WebRequest');
clr.AddReference('System.Numerics');
clr.AddReference('System.Numerics.Vectors');
clr.AddReference('System.Reflection.Context');
clr.AddReference('System.Runtime.Caching');
clr.AddReference('System.Runtime.DurableInstancing');
clr.AddReference('System.Runtime.Remoting');
clr.AddReference('System.Runtime.Serialization');
clr.AddReference('System.Runtime.Serialization.Formatters.Soap');
clr.AddReference('System.Security');
clr.AddReference('System.ServiceModel.Activation');
clr.AddReference('System.ServiceModel.Discovery');
clr.AddReference('System.ServiceModel');
clr.AddReference('System.ServiceModel.Internals');
clr.AddReference('System.ServiceModel.Routing');
clr.AddReference('System.ServiceModel.Web');
clr.AddReference('System.ServiceProcess');
clr.AddReference('System.Transactions');
clr.AddReference('System.Web.Abstractions');
clr.AddReference('System.Web.ApplicationServices');
clr.AddReference('System.Web');
clr.AddReference('System.Web.DynamicData');
clr.AddReference('System.Web.Extensions.Design');
clr.AddReference('System.Web.Extensions');
clr.AddReference('System.Web.Http');
clr.AddReference('System.Web.Http.SelfHost');
clr.AddReference('System.Web.Http.WebHost');
clr.AddReference('System.Web.Mvc');
clr.AddReference('System.Web.Razor');
clr.AddReference('System.Web.RegularExpressions');
clr.AddReference('System.Web.Routing');
clr.AddReference('System.Web.Services');
clr.AddReference('System.Web.WebPages.Deployment');
clr.AddReference('System.Web.WebPages');
clr.AddReference('System.Web.WebPages.Razor');
clr.AddReference('System.Windows.Forms.DataVisualization');
clr.AddReference('System.Windows.Forms');
clr.AddReference('System.Xaml');
clr.AddReference('System.Xml');
clr.AddReference('System.Xml.Linq');
clr.AddReference('SystemWebTestShim');
clr.AddReference('UnhollowerBaseLib');
clr.AddReference('UnhollowerRuntimeLib');
clr.AddReference('Unity.Addressables');
clr.AddReference('Unity.Compat');
clr.AddReference('Unity.ResourceManager');
clr.AddReference('Unity.Tasks');
clr.AddReference('Unity.TextMeshPro');
clr.AddReference('UnityEngine.AIModule');
clr.AddReference('UnityEngine.AndroidJNIModule');
clr.AddReference('UnityEngine.AnimationModule');
clr.AddReference('UnityEngine.AssetBundleModule');
clr.AddReference('UnityEngine.AudioModule');
clr.AddReference('UnityEngine.CoreModule');
clr.AddReference('UnityEngine.CrashReportingModule');
clr.AddReference('UnityEngine.DirectorModule');
clr.AddReference('UnityEngine');
clr.AddReference('UnityEngine.GameCenterModule');
clr.AddReference('UnityEngine.GridModule');
clr.AddReference('UnityEngine.Il2CppAssetBundleManager');
clr.AddReference('UnityEngine.ImageConversionModule');
clr.AddReference('UnityEngine.IMGUIModule');
clr.AddReference('UnityEngine.InputLegacyModule');
clr.AddReference('UnityEngine.InputModule');
clr.AddReference('UnityEngine.JSONSerializeModule');
clr.AddReference('UnityEngine.ParticleSystemModule');
clr.AddReference('UnityEngine.Physics2DModule');
clr.AddReference('UnityEngine.PhysicsModule');
clr.AddReference('UnityEngine.Purchasing');
clr.AddReference('UnityEngine.SharedInternalsModule');
clr.AddReference('UnityEngine.SubsystemsModule');
clr.AddReference('UnityEngine.TerrainModule');
clr.AddReference('UnityEngine.TextCoreModule');
clr.AddReference('UnityEngine.TextRenderingModule');
clr.AddReference('UnityEngine.TilemapModule');
clr.AddReference('UnityEngine.UI');
clr.AddReference('UnityEngine.UIElementsModule');
clr.AddReference('UnityEngine.UIModule');
clr.AddReference('UnityEngine.UnityAnalyticsModule');
clr.AddReference('UnityEngine.UnityWebRequestAssetBundleModule');
clr.AddReference('UnityEngine.UnityWebRequestModule');
clr.AddReference('UnityEngine.UnityWebRequestWWWModule');
clr.AddReference('UnityEngine.VFXModule');
clr.AddReference('UnityEngine.VideoModule');
clr.AddReference('UnityEngine.VRModule');
clr.AddReference('UnityEngine.XRModule');
clr.AddReference('UnityStore');
clr.AddReference('ValueTupleBridge');
clr.AddReference('WindowsBase');
" + script;
                #endregion

                ScriptSource source = pyEngine.CreateScriptSourceFromString(script, SourceCodeKind.Statements);
                CompiledCode compiled = source.Compile();
                // Executes in the scope of Python
                compiled.Execute(pyScope);
                return true;
            }catch(Exception ex)
            {
                Logger.instance.Log("Exception occoured when executing python code!");
                Logger.instance.Log(ex.Message);
                Logger.instance.Log(ex.StackTrace);
                return false;
            }
        }
    }
}
