using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class LevelUpPanel : BasePanel
{

    private Text txtLevelText;

    private float delayTime = 3;
    private float nowTime;

    private GameObject levelUpEffect;

    protected override void Awake()
    {
        base.Awake();

        nowTime = delayTime;

        txtLevelText = this.transform.Find("LevelUpBK/LevelUpText").GetComponent<Text>();
    }

    protected override void Update()
    {
        base.Update();

        

        if(nowTime <= 0)
        {
            nowTime = delayTime;

            UIManager.Instance.CloseThisPanel<LevelUpPanel>(true);
        }
        else
        {
            nowTime -= Time.deltaTime;
        }
    }

    public override void Init()
    {
        
    }

    public void ShowLevelUp(int nowLevel)
    {
        txtLevelText.text = nowLevel.ToString();

        if(levelUpEffect == null)
        {
            GameObject levelObj = AssetBundleMgr.Instance.LoadAsset<GameObject>("effect", "LevelUp");
            levelObj = GameObject.Instantiate(levelObj);
            //ResourceRequest rq = Resources.LoadAsync<GameObject>("Effect/LevelUp");

            //levelUpEffect = GameObject.Instantiate(rq.asset as GameObject);
        }
    }

    public override void CloseThisPanel(UnityAction unityAction = null)
    {
        base.CloseThisPanel(unityAction);
    }
}
