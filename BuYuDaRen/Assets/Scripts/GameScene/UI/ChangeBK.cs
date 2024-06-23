using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeBK : MonoBehaviour
{

    public List<Sprite> sprites;

    private Image BK_1;
    private Image BK_2;

    private bool isShow = false;

    private static ChangeBK instance;
    public static ChangeBK Instance => instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        BK_1 = this.transform.Find("BK").GetComponent<Image>();
        BK_2 = this.transform.Find("BK2").GetComponent<Image>();

        AssetBundle bk = AssetBundleMgr.Instance.GetAB("bk");

        Sprite[] sprites1 = bk.LoadAllAssets<Sprite>();

        sprites.AddRange(sprites1);
    }

    // Update is called once per frame
    void Update()
    {
        if(isShow)
        {
            BK_2.fillAmount += 0.008F;

            if(BK_2.fillAmount >= 1f)
            {

                BK_1.sprite = BK_2.sprite;
                BK_2.fillAmount = 0;

                isShow = false;
            }
        }
    }

    public void BK(int index)
    {


        BK_1.sprite = sprites[index];

        if(index + 1 > sprites.Count - 1)
        {
            BK_2.sprite = sprites[0];
        }
        else
        {
            BK_2.sprite = sprites[index + 1];
        }
        

        BK_2.fillAmount = 0;

        isShow = true;
    }
}
