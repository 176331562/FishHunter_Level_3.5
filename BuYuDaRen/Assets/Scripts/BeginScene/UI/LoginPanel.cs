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

    }

    public override void Init()
    {
        rememberAccountToggle.onValueChanged.AddListener((b) =>
        {

        });


        rememberPassWordToggle.onValueChanged.AddListener((b) =>
        {

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
            LoginInfo loginInfo = GameDataMgr.Instane.loginInfos;

            for (int i = 0; i < loginInfo.loginDatas.Count; i++)
            {
                if(loginInfo.loginDatas[i].account == accountIP.text && loginInfo.loginDatas[i].password == passWordIP.text)
                {
                    UIManager.Instance.ShowThisPanel<TipPanel>().ChangeText("账号密码正确");

                    break;
                }
                else
                {
                    UIManager.Instance.ShowThisPanel<TipPanel>().ChangeText("没有此账号或账号密码错误");
                    accountIP.text = string.Empty;
                    passWordIP.text = string.Empty;
                }
            }
        });

        questionBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowThisPanel<TipPanel>().ChangeText("账号密码最多七位");
        });
    }
}
