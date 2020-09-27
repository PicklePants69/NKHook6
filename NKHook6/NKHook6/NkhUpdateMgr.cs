using Assets.Scripts.Models;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.UI_New.Popups;
using Newtonsoft.Json.Linq;
using NKHook6.Api.Utilities;
using NKHook6.Api.Web;
using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.Jobs;
using UnityEngine;

namespace NKHook6
{
    class NkhUpdateMgr
    {
        private static bool CheckedForUpdates = false;
        /// <summary>
        /// This method will check all of the loaded mods for updates and post notifications in console
        /// </summary>
        public static async Task HandleUpdates()
        {
            if (CheckedForUpdates)
                return;

            await Task.Run(() =>
            {
                foreach (var item in LatestVersionURLAttribute.loaded)
                {
                    var text = WebHandler.ReadText_FromURL(item.url);
                    if (String.IsNullOrEmpty(text))
                        return;

                    string cleanedLatest = "";
                    foreach (var letter in text)
                    {
                        if (Int32.TryParse(letter.ToString(), out int result))
                            cleanedLatest += result.ToString();
                    }

                    if (String.IsNullOrEmpty(cleanedLatest))
                        return;


                    var melonInfo = Utils.GetModInfo(item.type.Assembly);
                    string cleanedCurrent = "";
                    foreach (var letter in melonInfo.Version)
                    {
                        if (Int32.TryParse(letter.ToString(), out int result))
                            cleanedCurrent += result.ToString();
                    }

                    if (String.IsNullOrEmpty(cleanedCurrent))
                        return;


                    while (cleanedCurrent.Length != cleanedLatest.Length)
                    {
                        if (cleanedCurrent.Length < cleanedLatest.Length)
                            cleanedCurrent += "0";
                        else
                            cleanedLatest += "0";
                    }

                    int current = Int32.Parse(cleanedCurrent);
                    int latest = Int32.Parse(cleanedLatest);

                    if (latest > current)
                        Logger.Log("An update is available for " + melonInfo.Name + "!", Logger.Level.UpdateNotify, melonInfo.Name);
                    else
                        Logger.Log(melonInfo.Name + " is up to date", melonInfo.Name);

                    CheckedForUpdates = true;
                }
            }).ConfigureAwait(continueOnCapturedContext: true);
           
            //PopupScreen.instance.Invoke("ShowEventPopup", 10);

            /*if (isNkhUpdate)
                Logger.ShowMsgPopup("Update available!", "An update is available for NKHook6. Make sure to get it so you're using the latest features!");*/

            /*BgThread.AddToQueue(() =>
            {
                
            });*/
        }

        
    }
}