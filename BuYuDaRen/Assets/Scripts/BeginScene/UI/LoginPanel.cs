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
        //如果用户第一次登录时，这个数据表里面默认为null
        if(GameDataMgr.Instane.loginInfos == null)
        {
            return null;
        }

        LoginInfo loginInfo = GameDataMgr.Instane.loginInfos;

        for (int i = 0; i < loginInfo.loginDatas.Count; i++)
        {
            //Debug.LogError("账号" + loginInfo.loginDatas[i].account);
            //Debug.LogError("密码" + loginInfo.loginDatas[i].password);
            if (loginInfo.loginDatas[i].account == accountIP.text && loginInfo.loginDatas[i].password == passWordIP.text)
            {
                //保存当前是否保存账号/自动登录
                LoginData loginData = loginInfo.loginDatas[i];
                loginData.isRememberAccount = rememberAccountToggle.isOn;
                loginData.isRememberPassWord = rememberPassWordToggle.isOn;
                loginData.isShowFirst = rememberPassWordToggle.isOn;

                GameDataMgr.Instane.InitNowSelectLogin(loginData, i - 1);               

                GameDataMgr.Instane.SaveLoginData(loginData);

                ShowLoginAnimator();

                return loginData;
            }
            
        }
        //说明没有这个账号，要么账号密码错误
        UIManager.Instance.ShowThisPanel<TipPanel>().ChangeText("没有此账号或账号密码错误");
        accountIP.text = string.Empty;
        passWordIP.text = string.Empty;


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

        //如果啥也没有说明可能连文件都没有
        if(GameDataMgr.Instane.loginInfos.loginDatas.Count == 0)
        {
            LoginData logindata = new LoginData();

            GameDataMgr.Instane.SaveLoginData(logindata);            
        }    

        //默认也是没有音乐的，让它自己创建
        if(GameDataMgr.Instane.musicData == null)
        {
            MusicData musicData = new MusicData();

            GameDataMgr.Instane.SaveMusicData(musicData);
        }

        //如果有文件并且有用户数据的时候
        if(GameDataMgr.Instane.loginInfos.loginDatas.Count != 0)
        {
            //默认上来值为0！！
            int index = GameDataMgr.Instane.nowSelectIndex;

            //我们就直接遍历用户数据表看看就把第一个记住密码的作为默认值
            for (int i = 0; i < GameDataMgr.Instane.loginInfos.loginDatas.Count; i++)
            {
                //如果优先展示默认展示第一个
                if (GameDataMgr.Instane.loginInfos.loginDatas[i].isShowFirst)
                {
                    LoginData loginData = GameDataMgr.Instane.loginInfos.loginDatas[i];

                    rememberAccountToggle.isOn = loginData.isRememberAccount;
                    rememberPassWordToggle.isOn = loginData.isRememberPassWord;

                    if (loginData.isRememberAccount)
                    {
                        accountIP.text = loginData.account;
                        passWordIP.text = loginData.password;
                    }

                    return;
                }
            }

            //如果没有自动登录的账号就直接设置默认设置
            accountIP.text = string.Empty;
            passWordIP.text = string.Empty;

            rememberAccountToggle.isOn = false;
            rememberPassWordToggle.isOn = false;
        }
    }
}
