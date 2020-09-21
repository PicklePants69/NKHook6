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
        public static GameObject CreatePrefab(string assetBundlePath, string assetToLoad) => CreatePrefab(AssetBundle.LoadFromFile(assetBundlePath), assetToLoad);

        /// <summary>
        /// Create a GameObject prefab from an asset bundle. Used to add assets to the game.
        /// Asset bundle must already have been exproted from unity.
        /// More info on making asset bundles here: https://docs.unity3d.com/Manual/AssetBundles-Workflow.html
        /// </summary>
        /// <param name="assetBundle">The AssetBundle you want to make a prefab out of</param>
        /// <param name="assetToLoad">The specific asset you want to load from the asset bundle.
        /// The GameObject prefab will be this part of the asset bundle</param>
        /// <returns></returns>
        public static GameObject CreatePrefab(AssetBundle assetBundle, string assetToLoad) => assetBundle.LoadAsset(assetToLoad).Cast<GameObject>();
    }
}
