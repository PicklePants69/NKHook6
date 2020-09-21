using System;
using System.IO;
using UnityEngine;

namespace NKHook6.Api.Utilities
{
    //Taken from: https://github.com/SubnauticaModding/SMLHelper/blob/master/SMLHelper/Utility/ImageUtils.cs

    /// <summary>
    /// A collection of image loading utility methods that can create Unity objects from image files at runtime.
    /// </summary>
    public static class ImageUtils
    {
        /// <summary>
        /// Creates a new <see cref="Texture2D" /> from an image file.
        /// </summary>
        /// <param name="filePathToImage">The path to the image file.</param>
        /// <param name="format">
        /// <para>The texture format. By default, this uses <see cref="TextureFormat.BC7" />.</para>
        /// <para>https://docs.unity3d.com/ScriptReference/TextureFormat.BC7.html</para>
        /// <para>Don't change this unless you really know what you're doing.</para>
        /// </param>
        /// <returns>Will return a new <see cref="Texture2D"/> instance if the file exists; Otherwise returns null.</returns>
        /// <remarks>
        /// Ripped from: https://github.com/RandyKnapp/SubnauticaModSystem/blob/master/SubnauticaModSystem/Common/Utility/ImageUtils.cs
        /// </remarks>
        public static Texture2D LoadTextureFromFile(string filePathToImage, TextureFormat format = TextureFormat.BC7)
        {
            if (File.Exists(filePathToImage))
            {
                byte[] imageBytes = File.ReadAllBytes(filePathToImage);
                var texture2D = new Texture2D(2, 2, format, false);
                try
                {
                    texture2D.LoadRawTextureData(imageBytes);
                    return texture2D;
                }
                catch (Exception uex)
                {
                    Logger.Log("Error on LoadTextureFromFile call. Texture cannot be loaded: " + uex, Logger.Level.Error);
                }

            }
            else
            {
                Logger.Log("Error on LoadTextureFromFile call. File not found at " + filePathToImage, Logger.Level.Error);
            }

            return null;
        }
    }
}
