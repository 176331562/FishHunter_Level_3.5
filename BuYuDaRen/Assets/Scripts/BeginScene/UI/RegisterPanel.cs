using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RegisterPanel : BasePanel
{

    private InputField accountIP;
    private InputField passWordIP;

    private Button btnFalse;
    private Button btnTrue;
    private Button btnQuestion;

    protected override void Awake()
    {
        base.Awake();

        accountIP = this.transform.Find("AccountNum").GetComponent<InputField>();
        passWordIP = this.transform.Find("Password").GetComponent<InputField>();

        btnFalse = this.transform.Find("FalseBtn").GetComponent<Button>();
        btnTrue = this.transform.Find("TrueBtn").GetComponent<Button>();
        btnQuestion = this.transform.Find("QuestionBtn").GetComponent<Button>();
    }

    public override void Init()
    {
        accountIP.onValueChanged.AddListener((v) =>
        {
            if(v.Length > 7)
            {
                AcAndPwWroung();

                accountIP.text = string.Empty;
            }

        });

        passWordIP.onValueChanged.AddListener((v) =>
        {
            if (v.Length > 7)
            {
                AcAndPwWroung();

                passWordIP.text = string.Empty;
            }
        });

        btnQuestion.onClick.AddListener(() =>
        {
            AcAndPwWroung();
        });

        btnTrue.onClick.AddListener(() =>
        {
            RegisterAccount();
        });

        btnFalse.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseThisPanel<RegisterPanel>(true, () =>
            {
                UIManager.Instance.ShowThisPanel<LoginPanel>();
            });
        });
    }

    //账号密码超出长度或有问题
    private void AcAndPwWroung()
    {
        UIManager.Instance.ShowThisPanel<TipPanel>().ChangeText("账号密码长度不能大于七位");
    }


    //注册账号
    private void RegisterAccount()
    {
        //第一次注册时是没有登录数据的
        if(GameDataMgr.Instane.loginInfos == null)
        {
            LoginData loginData = new LoginData();
            GameDataMgr.Instane.SaveLoginData(loginData);
        }

        LoginInfo loginInfo = GameDataMgr.Instane.loginInfos;

        for (int i = 0; i < loginInfo.loginDatas.Count; i++)
        {
            if (loginInfo.loginDatas[i].account == accountIP.text)
            {
                UIManager.Instance.ShowThisPanel<TipPanel>().ChangeText("账号已被注册");

                accountIP.text = string.Empty;
                passWordIP.text = string.Empty;

                return;
            }

           

        }

        //如果当前账号没注册过
        if (accountIP.text != string.Empty && accountIP.text.Length < 7)
        {

           
             //并且列表里面没有注册过当前账号
                
              //并且密码长度正确
                    if (passWordIP.text != string.Empty && passWordIP.text.Length < 7)
                    {
                        LoginData loginData = new LoginData();
                        loginData.account = accountIP.text;
                        loginData.password = passWordIP.text;

                        GameDataMgr.Instane.SaveLoginData(loginData);


                        UIManager.Instance.CloseThisPanel<RegisterPanel>(true, () =>
                        {
                            UIManager.Instance.ShowThisPanel<LoginPanel>();
                            UIManager.Instance.ShowThisPanel<TipPanel>().ChangeText("注册成功");


                        });

                        
                    }
                
           
        }
    }
}
