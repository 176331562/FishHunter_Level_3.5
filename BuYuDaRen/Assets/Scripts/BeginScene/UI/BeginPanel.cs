using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        });

        btnSetting.onClick.AddListener(() =>
        {

        });


        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
