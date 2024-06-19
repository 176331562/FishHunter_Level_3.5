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
    }


    public override void Init()
    {
        moneyBtn.onClick.AddListener(() =>
        {

        });

        btnLeft.onClick.AddListener(() =>
        {

        });

        btnRight.onClick.AddListener(() =>
        {

        });

        btnBack.onClick.AddListener(() =>
        {

        });
    }

    public void InitPanel(PlayerData playerData)
    {
        levelText.text = playerData.level.ToString();
        levelName.text = playerData.levelName;
        expImg.fillAmount = playerData.exp/10;

        money.text = playerData.gold.ToString();
        smallTimeText.text = "60";
    }
}
