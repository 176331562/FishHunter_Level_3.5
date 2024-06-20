using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePanel : BasePanel
{

    private Text levelText;
    private Text levelName;
    private Image expImg;

    private Text money;
    private Text smallTimeText;

    private Button moneyBtn;

    private Button btnLeft;
    private Button btnRight;

    private Button btnBack;

    private GunFather shootFather;


    protected override void Awake()
    {
        base.Awake();

        levelText = this.transform.Find("LevelBK/LevelText").GetComponent<Text>();
        levelName = this.transform.Find("LevelBK/NameText").GetComponent<Text>();
        expImg = this.transform.Find("LevelBK/ExpImg").GetComponent<Image>();

        money = this.transform.Find("MoneyBK/Money").GetComponent<Text>();
        smallTimeText = this.transform.Find("MoneyBK/smallTimer").GetComponent<Text>();

        moneyBtn = this.transform.Find("CountDownBK/btnMoney").GetComponent<Button>();

        btnLeft = this.transform.Find("ShootBK/LeftBtn").GetComponent<Button>();
        btnRight = this.transform.Find("ShootBK/RightBtn").GetComponent<Button>();

        btnBack = this.transform.Find("BackBtn").GetComponent<Button>();

        shootFather = GameObject.FindGameObjectWithTag("ShootFather").GetComponent<GunFather>();
    }


    public override void Init()
    {
        moneyBtn.onClick.AddListener(() =>
        {

        });

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
            GameDataMgr.Instane.SavePlayerData(GameDataMgr.Instane.nowSelectPlayerData);
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
    }

    public void ChangeGold(int gold)
    {
        money.text = gold.ToString();
    }

    public void ChangeLevel(int exp,int level)
    {
        PlayerData nowSelectPlayerData = GameDataMgr.Instane.nowSelectPlayerData;

        int nextLevelExp = 1000 * nowSelectPlayerData.level;

        if(exp > nextLevelExp)
        {
            level += (int)exp / nextLevelExp;

            exp -= nextLevelExp;

            expImg.fillAmount = (float)(exp / nextLevelExp);

            GameDataMgr.Instane.nowSelectPlayerData.level = level;
            GameDataMgr.Instane.nowSelectPlayerData.exp = exp;
        }
       
        expImg.fillAmount = ((float)exp / nextLevelExp);

        levelText.text = level.ToString();
    }
}
