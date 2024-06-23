using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BeginPanel : BasePanel
{
    private Button btnStart;
    private Button btnSetting;
    private Button btnQuit;

    protected override void Awake()
    {
        base.Awake();

        btnStart = this.transform.Find("BeginBG/BtnFather/StartBtn").GetComponent<Button>();
        btnSetting = this.transform.Find("BeginBG/BtnFather/SettingBtn").GetComponent<Button>();
        btnQuit = this.transform.Find("BeginBG/BtnFather/QuitBtn").GetComponent<Button>();
    }

    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseThisPanel<BeginPanel>(true);

            AsyncOperation ao = SceneManager.LoadSceneAsync("GameScene");
            ao.completed += (ao) =>
            {
                AssetBundleMgr.Instance.GetAB("fish");
                AssetBundleMgr.Instance.GetAB("web");
                AssetBundleMgr.Instance.GetAB("gun");
                AssetBundleMgr.Instance.GetAB("bullets");
                AssetBundleMgr.Instance.GetAB("coin");
                AssetBundleMgr.Instance.GetAB("effect");
                AssetBundleMgr.Instance.GetAB("seawave");
                AssetBundleMgr.Instance.GetAB("sound");
            };
        });

        btnSetting.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseThisPanel<BeginPanel>(true, () =>
            {
                UIManager.Instance.ShowThisPanel<SettingPanel>();
            });
        });


        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
