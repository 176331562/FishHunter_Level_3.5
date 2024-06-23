using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
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
        {         
            return loadedAssetBundle[assetBundleName].LoadAssetAsync<T>(assetName).asset as T;
        }
            

        //如果没有就直接加载
        if(!loadedAssetBundle.ContainsKey(assetBundleName))
        {
            AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundle/" + assetBundleName);

            AssetBundleRequest ar = ab.LoadAssetAsync<T>(assetName);

            ar.completed += (ao) =>
            {
                Debug.LogError(132);
            };

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

            //LoadDependBundle(assetBundleName);

            return ab;
        }

        return null;
    }

    //加载依赖包
    public void LoadDependBundle(string bundleName)
    {
        if(!loadedAssetBundle.ContainsKey("assetBundle"))
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundle/AssetBundle");

            AssetBundleManifest assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            loadedAssetBundle.Add("assetBundle", assetBundle);

            string[] dependBundles = assetBundleManifest.GetAllDependencies(bundleName);

            if(dependBundles != null)
            {
                Debug.LogError("加载依赖包");

                for (int i = 0; i < dependBundles.Length; i++)
                {
                    if (!loadedAssetBundle.ContainsKey(dependBundles[i]))
                    {
                        AssetBundleCreateRequest acr = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/AssetBundle/" + bundleName);

                        AssetBundle ab = acr.assetBundle;

                        loadedAssetBundle.Add(bundleName, ab);
                    }
                }

            }

        }
    }

  
}
