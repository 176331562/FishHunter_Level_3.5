using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleMgr
{
    private static AssetBundleMgr instance;

    public static AssetBundleMgr Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new AssetBundleMgr();
            }
            return instance;
        }
    }

    private Dictionary<string, AssetBundle> loadedAssetBundle;

    private AssetBundleMgr()
    {
        loadedAssetBundle = new Dictionary<string, AssetBundle>();

    }

    public T LoadAsset<T>(string assetBundleName, string assetName) where T : class
    {
       
        //如果字典里面已经有当前的ab包了就直接返回
        if (loadedAssetBundle.ContainsKey(assetBundleName))
            return loadedAssetBundle[assetBundleName].LoadAssetAsync<T>(assetName).asset as T;

        //如果没有就直接加载
        if(!loadedAssetBundle.ContainsKey(assetBundleName))
        {
            AssetBundleCreateRequest acr = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/AssetBundle/" + assetBundleName);

            AssetBundle ab = acr.assetBundle;

            AssetBundleRequest ar = ab.LoadAssetAsync<T>(assetName);

            loadedAssetBundle.Add(assetBundleName, ab);

            return ar.asset as T;
        }

        return null;
    }

    public AssetBundle GetAB(string assetBundleName)
    {
    
        //如果字典里面已经有当前的ab包了就直接返回
        if (loadedAssetBundle.ContainsKey(assetBundleName))
            return loadedAssetBundle[assetBundleName];

        //如果没有就直接加载
        if (!loadedAssetBundle.ContainsKey(assetBundleName))
        {
            AssetBundleCreateRequest acr = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/AssetBundle/" + assetBundleName);

            AssetBundle ab = acr.assetBundle;
          
            loadedAssetBundle.Add(assetBundleName, ab);

            return ab;
        }

        return null;
    }
}
