using NKHook6.Utils;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace NKHook6.Api
{
    public class ModSettings
    {
        const string modsFolder = "\\Mods\\";
        string FileName = "settings.json";
        string FilePath = "";
        public ModSettings()
        {
            string modName = Toolkit.GetCallingModInfo().Name;

            if (!String.IsNullOrEmpty(modName))
            {
                FilePath = Environment.CurrentDirectory + modsFolder + modName;

                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);

                FilePath += "\\" + FileName;
            }
        }

        public ModSettings(string fileName)
        {
            if (fileName.StartsWith("\\") || fileName.StartsWith("/"))
                fileName = fileName.TrimStart('\\').TrimStart('/');

            FileName = fileName;
        }



        //Taken from: https://github.com/SubnauticaModding/SMLHelper/blob/master/SMLHelper/Utility/JsonUtils.cs
        /// <summary>
        /// A collection of utilities for interacting with JSON files.
        /// </summary>
        public static class JsonUtils
        {
            private static readonly string modsFolder = "\\Mods\\";
            private static string FileName = "settings.json";

            private static string GetDefaultPath()
            {
                string result = "";
                string modName = Toolkit.GetCallingModInfo().Name;

                if (!String.IsNullOrEmpty(modName))
                {
                    result = Environment.CurrentDirectory + modsFolder + modName;

                    if (!Directory.Exists(result))
                        Directory.CreateDirectory(result);

                    result += "\\" + FileName;
                }
                return result;
            }

            /// <summary>
            /// Create an instance of <typeparamref name="T"/>, populated with data from the JSON file at the given 
            /// <paramref name="path"/>.
            /// </summary>
            /// <typeparam name="T">The type of object to initialise and populate with JSON data.</typeparam>
            /// <param name="path">The path on disk at which the JSON file can be found.</param>
            /// <param name="createFileIfNotExist">Whether a new JSON file should be created with default values if it does not 
            /// already exist.</param>
            /// <param name="jsonConverters">An array of <see cref="JsonConverter"/>s to be used for deserialization.</param>
            /// <returns>The <typeparamref name="T"/> instance populated with data from the JSON file at
            /// <paramref name="path"/>, or default values if it cannot be found or an error is encountered while parsing the
            /// file.</returns>
            /// <seealso cref="Load{T}(T, string, bool, JsonConverter[])"/>
            /// <seealso cref="Save{T}(T, string, JsonConverter[])"/>
            public static T Load<T>(string path = null, bool createFileIfNotExist = true,
                params JsonConverter[] jsonConverters) where T : class, new()
            {
                if (string.IsNullOrEmpty(path))
                {
                    path = GetDefaultPath();
                }

                if (Directory.Exists(Environment.CurrentDirectory + "\\Mods") && File.Exists(path))
                {
                    try
                    {
                        string serializedJson = File.ReadAllText(path);
                        return JsonConvert.DeserializeObject<T>(
                            serializedJson, jsonConverters
                        );
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"Could not parse JSON file, loading default values: {path}", Logger.Level.Warning);
                        Logger.Log(ex.Message);
                        Logger.Log(ex.StackTrace);
                        return new T();
                    }
                }
                else if (createFileIfNotExist)
                {
                    T jsonObject = new T();
                    Save(jsonObject, path, jsonConverters);
                    return jsonObject;
                }
                else
                {
                    return new T();
                }
            }

            /// <summary>
            /// Loads data from the JSON file at <paramref name="path"/> into the <paramref name="jsonObject"/>.
            /// </summary>
            /// <typeparam name="T">The type of <paramref name="jsonObject"/> to populate with JSON data.</typeparam>
            /// <param name="jsonObject">The <typeparamref name="T"/> instance to popular with JSON data.</param>
            /// <param name="path">The path on disk at which the JSON file can be found.</param>
            /// <param name="createFileIfNotExist">Whether a new JSON file should be created with default values if it does not
            /// already exist.</param>
            /// <param name="jsonConverters">An array of <see cref="JsonConverter"/>s to be used for deserialization.</param>
            /// <seealso cref="Load{T}(string, bool, JsonConverter[])"/>
            /// <seealso cref="Save{T}(T, string, JsonConverter[])"/>
            public static void Load<T>(T jsonObject, string path = null, bool createFileIfNotExist = true,
                params JsonConverter[] jsonConverters) where T : class
            {
                if (string.IsNullOrEmpty(path))
                {
                    path = GetDefaultPath();
                }

                if (Directory.Exists(Environment.CurrentDirectory + "\\Mods") && File.Exists(path))
                {
                    try
                    {
                        var jsonSerializerSettings = new JsonSerializerSettings()
                        {
                            Converters = jsonConverters
                        };

                        string serializedJson = File.ReadAllText(path);
                        JsonConvert.PopulateObject(
                            serializedJson, jsonObject, jsonSerializerSettings
                        );
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"Could not parse JSON file, instance values unchanged: {path}", Logger.Level.Warning);
                        Logger.Log(ex.Message);
                        Logger.Log(ex.StackTrace);
                    }
                }
                else if (createFileIfNotExist)
                {
                    Save(jsonObject, path, jsonConverters);
                }
            }

            /// <summary>
            /// Saves the <paramref name="jsonObject"/> parsed as JSON data to the JSON file at <paramref name="path"/>,
            /// creating it if it does not exist.
            /// </summary>
            /// <typeparam name="T">The type of <paramref name="jsonObject"/> to parse into JSON data.</typeparam>
            /// <param name="jsonObject">The <typeparamref name="T"/> instance to parse into JSON data.</param>
            /// <param name="path">The path on disk at which to store the JSON file.</param>
            /// <param name="jsonConverters">An array of <see cref="JsonConverter"/>s to be used for serialization.</param>
            /// <seealso cref="Load{T}(T, string, bool, JsonConverter[])"/>
            /// <seealso cref="Load{T}(string, bool, JsonConverter[])"/>
            public static void Save<T>(T jsonObject, string path = null,
                params JsonConverter[] jsonConverters) where T : class
            {
                if (string.IsNullOrEmpty(path))
                {
                    path = GetDefaultPath();
                }

                var stringBuilder = new StringBuilder();
                var stringWriter = new StringWriter(stringBuilder);
                using (var jsonTextWriter = new JsonTextWriter(stringWriter)
                {
                    Indentation = 4,
                    Formatting = Formatting.Indented
                })
                {
                    var jsonSerializer = new JsonSerializer();
                    foreach (var jsonConverter in jsonConverters)
                    {
                        jsonSerializer.Converters.Add(jsonConverter);
                    }
                    jsonSerializer.Serialize(jsonTextWriter, jsonObject);
                }

                var fileInfo = new FileInfo(path);
                fileInfo.Directory.Create(); // Only creates the directory if it doesn't already exist
                File.WriteAllText(path, stringBuilder.ToString());
            }
        }
    }
}
