using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadPanel : BasePanel
{
    public override void Init()
    {
        StartCoroutine(LoadAb());
    }


    IEnumerator LoadAb()
    {
        //UIManager.Instance.ShowThisPanel<LoadPanel>();

        yield return new WaitForSeconds(1);

        AssetBundleMgr.Instance.GetAB("fish");
        AssetBundleMgr.Instance.GetAB("web");
        AssetBundleMgr.Instance.GetAB("gun");
        AssetBundleMgr.Instance.GetAB("bullets");
        AssetBundleMgr.Instance.GetAB("coin");
        AssetBundleMgr.Instance.GetAB("effect");
        AssetBundleMgr.Instance.GetAB("seawave");
        AssetBundleMgr.Instance.GetAB("sound");

        yield return new WaitForSeconds(2);

        //

        AsyncOperation ao = SceneManager.LoadSceneAsync("GameScene");
        ao.completed += (ao) =>
        {
            //UIManager.Instance.CloseThisPanel<LoadPanel>();

            UIManager.Instance.RemovePanel<LoadPanel>();
        };
    }
}
