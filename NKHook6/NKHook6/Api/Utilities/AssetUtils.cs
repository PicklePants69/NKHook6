using Il2CppSystem.IO;
using UnityEngine;

namespace NKHook6.Api.Utilities
{
    public class AssetUtils
    {
        /// <summary>
        /// Create a GameObject prefab from an asset bundle. Used to add assets to the game.
        /// Asset bundle must already have been exproted from unity.
        /// More info on making asset bundles here: https://docs.unity3d.com/Manual/AssetBundles-Workflow.html
        /// </summary>
        /// <param name="assetBundlePath">The filepath to the asset bundle</param>
        /// <param name="assetToLoad">The specific asset you want to load from the asset bundle.
        /// The GameObject prefab will be this part of the asset bundle</param>
        /// <returns>GameObject prefab</returns>
        public static GameObject CreatePrefab(string assetBundlePath, string assetToLoad)
        {
            if (!File.Exists(assetBundlePath))
            {
                Logger.Log("Error! Failed to create asset bundle because no file exists at: " + assetBundlePath);
                return null;
            }
            
            return CreatePrefab(AssetBundle.LoadFromFile(assetBundlePath), assetToLoad);
        }

        /// <summary>
        /// Create a GameObject prefab from an asset bundle. Used to add assets to the game.
        /// Asset bundle must already have been exproted from unity.
        /// More info on making asset bundles here: https://docs.unity3d.com/Manual/AssetBundles-Workflow.html
        /// </summary>
        /// <param name="assetBundle">The AssetBundle you want to make a prefab out of</param>
        /// <param name="assetToLoad">The specific asset you want to load from the asset bundle.
        /// The GameObject prefab will be this part of the asset bundle</param>
        /// <returns></returns>
        public static GameObject CreatePrefab(AssetBundle assetBundle, string assetToLoad)
        {
            if (assetBundle == null)
            {
                Logger.Log("Error! AssetBundle is null: " + assetBundle.name);
                return null;
            }

            var result = assetBundle.LoadAsset(assetToLoad).Cast<GameObject>();

            if (result == null)
            {
                Logger.Log("Error! Failed to create GameObject prefab from AssetBundle. GameObject prefab is null: " + result.name);
                return null;
            }

            return result;
        }
    }
}
