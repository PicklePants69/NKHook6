using NKHook6.Api.Utilities;
using NKHook6.Api.Web;
using System;
using System.Security.Policy;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace NKHook6
{
    class UpdateMgr
    {
        /// <summary>
        /// This method will check all of the loaded mods for updates and post notifications in console
        /// </summary>
        public static async void HandleUpdates()
        {
            await Task.Run(() =>
            {
                foreach (var item in LatestVersionURLAttribute.loaded)
                {
                    var melonInfo = Utils.GetModInfo(item.type.Assembly);

                    UpdateHandler u = new UpdateHandler()
                    {
                        VersionURL = item.url,
                        ProjectName = melonInfo.Name,
                        CurrentVersion = melonInfo.Version
                    };
                    u.HandleUpdates(false, false);
                }
            });//.ConfigureAwait(continueOnCapturedContext: true);
        }
    }
}