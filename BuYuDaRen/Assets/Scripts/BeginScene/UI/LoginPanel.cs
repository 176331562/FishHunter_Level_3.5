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
        
    }
}
