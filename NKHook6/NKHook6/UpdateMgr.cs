using NKHook6.Api.Utilities;
using NKHook6.Api.Web;
using System;
using System.Threading.Tasks;

namespace NKHook6
{
    class UpdateMgr
    {
        /// <summary>
        /// This method will check all of the loaded mods for updates and post notifications in console
        /// </summary>
        public static void HandleUpdates()
        {
            await Task.Run(() =>
            {

                var text = "";
                try
                {
                    text = WebHandler.ReadText_FromURL(item.url);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message);
                    return;
                }
                if (String.IsNullOrEmpty(text))
                    return;

                string cleanedLatest = CleanVersionText(text);
                var melonInfo = Utils.GetModInfo(item.type.Assembly);
                string cleanedCurrent = CleanVersionText(melonInfo.Version);


                while (cleanedCurrent.Length != cleanedLatest.Length)
                {
                    if (cleanedCurrent.Length < cleanedLatest.Length)
                        cleanedCurrent += "0";
                    else
                        Logger.Log(melonInfo.Name + " is up to date", melonInfo.Name);
                }

                int current = Int32.Parse(cleanedCurrent);
                int latest = Int32.Parse(cleanedLatest);

                if (latest > current)
                    Logger.Log("An update is available for " + melonInfo.Name + "!", Logger.Level.UpdateNotify, melonInfo.Name);
                else
                    Logger.Log(melonInfo.Name + " is up to date", melonInfo.Name);

                CheckedForUpdates = true;
            }
        }

        /// <summary>
        /// Remove all non-numeric characters from version info
        /// </summary>
        /// <param name="uncleanedText"></param>
        /// <returns></returns>
        internal static string CleanVersionText(string uncleanedText)
        {
            string cleaned = "";
            foreach (var letter in uncleanedText)
            {
                if (Int32.TryParse(letter.ToString(), out int result))
                    cleaned += result.ToString();
            }

            return cleaned;
        }
    }
}