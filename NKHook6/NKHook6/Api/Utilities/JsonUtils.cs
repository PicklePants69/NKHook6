using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Utilities
{
    public class JsonUtils
    {
        public string FilePath = null;

        
        /*private JsonUtils instance;

        public JsonUtils Instance
        {
            get
            {
                if (instance == null)
                    instance = new JsonUtils();
                return instance;
            }
            set { instance = value; }
        }

        public JsonUtils()
        {

        }

        public JsonUtils(string filePath)
        {
            FilePath = filePath;

            var dir = new FileInfo(FilePath).Directory;
            if (!dir.Exists)
                dir.Create();

            //Load();
        }



        /// <summary>
        /// Load Json from file
        /// </summary>
        public void Load()
        {
            if (String.IsNullOrEmpty(FilePath) || !File.Exists(FilePath))
                return;

            string json = File.ReadAllText(FilePath);
            if (String.IsNullOrEmpty(json) || !IsJsonValid(json))
                return;

            instance = JsonConvert.DeserializeObject<JsonUtils>(json);

            var args = new JsonUtilsEventArgs();
            args.FilePath = FilePath;
            args.Instance = Instance;
            OnJsonLoaded(args);
        }

        /// <summary>
        /// Save Json to file
        /// </summary>
        public void Save()
        {
            string output = JsonConvert.SerializeObject(this, Formatting.Indented);

            StreamWriter serialize = new StreamWriter(FilePath, false);
            serialize.Write(output);
            serialize.Close();

            var args = new JsonUtilsEventArgs();
            args.FilePath = FilePath;
            args.Instance = Instance;
            OnJsonLoaded(args);
        }


        /// <summary>
        /// Check if FileInfo file contains valid json
        /// </summary>
        /// <param name="file">FileInfo to check</param>
        /// <returns>Whether or not FileInfo file contains valid json</returns>
        public static bool IsJsonValid(FileInfo file) => IsJsonValid(File.ReadAllText(file.FullName));

        /// <summary>
        /// Check if text is valid json
        /// </summary>
        /// <param name="text">Text to check</param>
        /// <returns>Whether or not text is valid json</returns>
        public static bool IsJsonValid(string text)
        {
            try
            {
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                dynamic result = serializer.DeserializeObject(text);
                return true;
            }
            catch { return false; }
        }


        #region Events

        /// <summary>
        /// Event fired when a Json file has successfully saved
        /// </summary>
        public static event EventHandler<JsonUtilsEventArgs> JsonSaved;

        /// <summary>
        /// Event fired when Json file has successfully loaded
        /// </summary>
        public static event EventHandler<JsonUtilsEventArgs> JsonLoaded;


        /// <summary>
        /// Events related to JetPasswords
        /// </summary>
        public class JsonUtilsEventArgs : EventArgs
        {
            /// <summary>
            /// Instance of Json file
            /// </summary>
            public JsonUtils Instance { get; set; }

            /// <summary>
            /// Filepath to the Json file
            /// </summary>
            public string FilePath { get; set; }
        }

        /// <summary>
        /// Fired when the Json file has successfully been loaded
        /// </summary>
        /// <param name="e">Event args containing the filepath to json file and insance of json file</param>
        public void OnJsonLoaded(JsonUtilsEventArgs e)
        {
            EventHandler<JsonUtilsEventArgs> handler = JsonLoaded;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Fired when the Json file has successfully been saved
        /// </summary>
        /// <param name="e">Event args containing the filepath to json file and insance of json file</param>
        public void OnJsonSaved(JsonUtilsEventArgs e)
        {
            EventHandler<JsonUtilsEventArgs> handler = JsonSaved;
            if (handler != null)
                handler(this, e);
        }
        #endregion


        #region Exceptions
        public class FilePathNotSet : Exception
        {
            public override string Message
            {
                get
                {
                    return "SavePath was not set for Json file!";
                }
            }
        }
        #endregion*/
    }
}
