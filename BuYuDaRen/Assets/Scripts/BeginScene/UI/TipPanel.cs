using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TipPanel : BasePanel
{

    private Text contentText;

    private Button btnClick;


    protected override void Awake()
    {
        base.Awake();

        contentText = this.transform.Find("TipImageBK/ContentText").GetComponent<Text>();

        btnClick = this.transform.Find("TipImageBK/TrueBtn").GetComponent<Button>();
    }


    public override void Init()
    {
        btnClick.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseThisPanel<TipPanel>(true);
        });
    }

    public void ChangeText(string text)
    {
        contentText.text = text;
    }
}
