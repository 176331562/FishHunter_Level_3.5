using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePanel : BasePanel
{

    private Text levelText;
    private Text levelName;
    private Image expImg;

    private Text money;
    private Text smallTimeText;

    private Text seaWaveText;

    private Button btnLeft;
    private Button btnRight;

    private Button btnBack;
    private Button btnSetting;

    //炮的父物体
    private GunFather shootFather;

    //没过60秒发一次零花钱
    private float smallTime = 60;
    private float nowSmallTime;

    //防止频繁读取造成内存消耗
    private List<LevelNameData> nowLevelNameList;


    //浪潮每四分钟生成一次
    private float seaWaveTime = 14;
    private float nowSeaWaveTime;

    protected override void Awake()
    {
        base.Awake();

        levelText = this.transform.Find("LevelBK/LevelText").GetComponent<Text>();
        levelName = this.transform.Find("LevelBK/NameText").GetComponent<Text>();
        expImg = this.transform.Find("LevelBK/ExpImg").GetComponent<Image>();

        money = this.transform.Find("MoneyBK/Money").GetComponent<Text>();
        smallTimeText = this.transform.Find("MoneyBK/smallTimer").GetComponent<Text>();

        seaWaveText = this.transform.Find("CountDownBK/SeaWave/TimeText").GetComponent<Text>();

        btnLeft = this.transform.Find("ShootBK/LeftBtn").GetComponent<Button>();
        btnRight = this.transform.Find("ShootBK/RightBtn").GetComponent<Button>();

        btnBack = this.transform.Find("BackBtn").GetComponent<Button>();
        btnSetting = this.transform.Find("SettingBtn").GetComponent<Button>();

        shootFather = GameObject.FindGameObjectWithTag("ShootFather").GetComponent<GunFather>();

        //当前零花钱时间
        nowSmallTime = smallTime;

        //当前浪潮时间
        nowSeaWaveTime = seaWaveTime;

        //称号列表
        nowLevelNameList = GameDataMgr.Instane.levelNameDatas;
    }

    protected override void Update()
    {
        base.Update();

        ChangeSmallTime();

        ChangeSeaWaveTime();

    }


    public override void Init()
    {
       

        btnLeft.onClick.AddListener(() =>
        {
            shootFather.LevelDownGun();
        });

        btnRight.onClick.AddListener(() =>
        {
            shootFather.LevelUpGun();
        });

        btnBack.onClick.AddListener(() =>
        {
            BackBeginScene();
        });

        btnSetting.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowThisPanel<SettingPanel>();
        });
    }


  

    public void InitPanel(PlayerData playerData)
    {
        levelText.text = playerData.level.ToString();
        levelName.text = playerData.levelName;
        int nextLevelExp = 1000 * playerData.level;
        expImg.fillAmount = ((float)playerData.exp / nextLevelExp);

        money.text = playerData.gold.ToString();
        smallTimeText.text = "60";

        ChangeLevelName(playerData.level);
    }

    public void ChangeGold(int gold)
    {
        money.text = gold.ToString();

        GameDataMgr.Instane.nowSelectPlayerData.gold = gold;
    }

    public void ChangeLevel(int exp,int level)
    {
        PlayerData nowSelectPlayerData = GameDataMgr.Instane.nowSelectPlayerData;

        int nextLevelExp = 1000 * nowSelectPlayerData.level;

        if(exp > nextLevelExp)
        {
            level += (int)exp / nextLevelExp;

            //显示升级UI
            UIManager.Instance.ShowThisPanel<LevelUpPanel>().ShowLevelUp(level);

            exp -= nextLevelExp;           
        }

        GameDataMgr.Instane.nowSelectPlayerData.level = level;
        GameDataMgr.Instane.nowSelectPlayerData.exp = exp;

        expImg.fillAmount = (((float)exp / nextLevelExp));
        //StartCoroutine(ExpLerp(expImg.fillAmount, ((float)exp / nextLevelExp)));

        ChangeLevelName(level);

        levelText.text = level.ToString();
    }


    //每60秒发一次零花钱
    public void ChangeSmallTime()
    {
        //开始倒计时
        if (nowSmallTime > 0)
        {
            nowSmallTime -= Time.deltaTime;
        }
        else
        {
            nowSmallTime = smallTime;

            //加零花钱的规则是按照当前等级*50
            int result = GameDataMgr.Instane.nowSelectPlayerData.gold + ((int)(GameDataMgr.Instane.nowSelectPlayerData.level * 10));

            ChangeGold(result);
        }

        smallTimeText.text = ((int)nowSmallTime).ToString();
    }

    //根据等级改变称号
    public void ChangeLevelName(int nowLevel)
    {
        //等级/10获得当前段位
        //int nowLevelIndex = (int)nowLevel / 10;

        //默认为1，/10会为0，防止是刚注册账号
        int nowLevelIndex = (int)nowLevel / 10 > 0 ? (int)nowLevel / 10 : 1;

        LevelNameData nowLevelNameData = nowLevelNameList[nowLevelIndex - 1];
        
        GameDataMgr.Instane.nowLevelIndex = nowLevelIndex;
        GameDataMgr.Instane.nowlevelNameData = nowLevelNameData;

        //如果当前保存的段位不是现在这个
        if(GameDataMgr.Instane.nowSelectPlayerData.levelName != nowLevelNameData.levelName)
        {
            GameDataMgr.Instane.nowSelectPlayerData.levelName = nowLevelNameData.levelName;
        }

        levelName.text = nowLevelNameData.levelName;
    }

    public void ChangeSeaWaveTime()
    {
        nowSeaWaveTime -= Time.deltaTime;

        if(nowSeaWaveTime <= 0)
        {
            nowSeaWaveTime = seaWaveTime;

            CreateSeaWave();
        }

        seaWaveText.text = ((int)nowSeaWaveTime).ToString();
    }

    //创建浪潮切换背景图
    public void CreateSeaWave()
    {
        ResourceRequest rq = Resources.LoadAsync<GameObject>("SeaWave/SeaWave");

        GameObject seaWaveObj = GameObject.Instantiate(rq.asset as GameObject);

        GameDataMgr.Instane.nowBKIndex = GameDataMgr.Instane.nowBKIndex+1 > ChangeBK.Instance.sprites.Count - 1 ? 0 : GameDataMgr.Instane.nowBKIndex+1;

        ChangeBK.Instance.BK(GameDataMgr.Instane.nowBKIndex);
    }

    //点击返回按钮就要保存当前的玩家数据并返回之前场景
    public void BackBeginScene()
    {
        //保存数据
        GameDataMgr.Instane.SavePlayerData(GameDataMgr.Instane.nowSelectPlayerData);

        //加载之前场景并显示开始界面
        AsyncOperation ao = SceneManager.LoadSceneAsync("BeginScene");

        UIManager.Instance.CloseThisPanel<GamePanel>(true);      
    }
}
