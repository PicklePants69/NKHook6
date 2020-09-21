using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NKHook6.Api.Utilities
{
    public class AssetUtils
    {
        public static GameObject CreatePrefab(string assetBundlePath, string assetToLoad) => CreatePrefab(AssetBundle.LoadFromFile(assetBundlePath), assetToLoad);
        public static GameObject CreatePrefab(AssetBundle assetBundle, string assetToLoad) => assetBundle.LoadAsset(assetToLoad).Cast<GameObject>();
    }
}
