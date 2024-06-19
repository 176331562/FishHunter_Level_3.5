using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    private InputField accountIP;
    private InputField passWordIP;

    private Toggle rememberAccountToggle;
    private Toggle rememberPassWordToggle;

    private Button registerBtn;
    private Button loginBtn;
    private Button questionBtn;

    private float delayTime = 4;

    private bool isLogin = false;

    //登录动画
    private GameObject loginAnimator;

    protected override void Awake()
    {
        base.Awake();

        accountIP = this.transform.Find("AccountNum").GetComponent<InputField>();
        passWordIP = this.transform.Find("Password").GetComponent<InputField>();

        rememberAccountToggle = this.transform.Find("RememberPWToggle").GetComponent<Toggle>();
        rememberPassWordToggle = this.transform.Find("AutoLoginToggle").GetComponent<Toggle>();

        registerBtn = this.transform.Find("RegisterBtn").GetComponent<Button>();
        loginBtn = this.transform.Find("LoginBtn").GetComponent<Button>();
        questionBtn = this.transform.Find("QuestionBtn").GetComponent<Button>();


        loginAnimator = this.transform.Find("LoginAnimator").gameObject;
    }

    protected override void Update()
    {
        base.Update();

        if(isLogin)
        {
            delayTime -= Time.deltaTime;

            if(delayTime <= 0)
            {
                isLogin = false;

                UIManager.Instance.CloseThisPanel<LoginPanel>(true, () =>
                {
                    UIManager.Instance.ShowThisPanel<BeginPanel>();
                });
            }
        }
    }

    public override void Init()
    {
        rememberAccountToggle.onValueChanged.AddListener((b) =>
        {
            //rememberPassWordToggle.isOn = b;
            if (!b)
                rememberPassWordToggle.isOn = b;
        });


        rememberPassWordToggle.onValueChanged.AddListener((b) =>
        {
            rememberAccountToggle.isOn = b;

        });

        registerBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseThisPanel<LoginPanel>(true, () =>
            {
                UIManager.Instance.ShowThisPanel<RegisterPanel>();
            });
        });

        loginBtn.onClick.AddListener(() =>
        {
            CheckLogin();

            
        });

        questionBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowThisPanel<TipPanel>().ChangeText("账号密码最多七位");
        });
    }

    //检查账号密码
    private LoginData CheckLogin()
    {
        LoginInfo loginInfo = GameDataMgr.Instane.loginInfos;

        for (int i = 0; i < loginInfo.loginDatas.Count; i++)
        {
            Debug.LogError("账号" + loginInfo.loginDatas[i].account);
            Debug.LogError("密码" + loginInfo.loginDatas[i].password);
            if (loginInfo.loginDatas[i].account == accountIP.text && loginInfo.loginDatas[i].password == passWordIP.text)
            {
                //保存当前是否保存账号/自动登录
                LoginData loginData = loginInfo.loginDatas[i];
                loginData.isRememberAccount = rememberAccountToggle.isOn;
                loginData.isRememberPassWord = rememberPassWordToggle.isOn;

                GameDataMgr.Instane.InitNowSelectLogin(loginData, i - 1);               

                GameDataMgr.Instane.SaveLoginData(loginData);

                ShowLoginAnimator();

                return loginData;
            }
            else if (loginInfo.loginDatas[i].account == accountIP.text && loginInfo.loginDatas[i].password != passWordIP.text)
            {
                UIManager.Instance.ShowThisPanel<TipPanel>().ChangeText("没有此账号或账号密码错误");
                accountIP.text = string.Empty;
                passWordIP.text = string.Empty;

                return null;
            }
            
        }

        return null;
    }

    //显示登录动画
    public void ShowLoginAnimator()
    {
        accountIP.interactable = false;
        passWordIP.interactable = false;

        rememberAccountToggle.gameObject.SetActive(false);
        rememberPassWordToggle.gameObject.SetActive(false);

        registerBtn.gameObject.SetActive(false);
        loginBtn.gameObject.SetActive(false);
        questionBtn.gameObject.SetActive(false);


        loginAnimator.SetActive(true);

        isLogin = true;
    }

    public override void ShowThisPanel()
    {
        base.ShowThisPanel();


        int index = GameDataMgr.Instane.nowSelectIndex;
        LoginData loginData = GameDataMgr.Instane.loginInfos.loginDatas[index];

        rememberAccountToggle.isOn = loginData.isRememberAccount;
        rememberPassWordToggle.isOn = loginData.isRememberPassWord;

        if (loginData.isRememberAccount)
        {
            accountIP.text = loginData.account;
            passWordIP.text = loginData.password;            
        }
    }
}
