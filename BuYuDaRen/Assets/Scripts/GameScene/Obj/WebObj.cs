using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebObj : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    private int atk;

   
    public void InitWeb(int layer,WebData webData)
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        spriteRenderer.sortingOrder = layer+1;
        this.atk = webData.damage;
    }

    public void DelayDestroy(float time)
    {
        Destroy(this.gameObject, time);
    }

    public void HurtFish(FishObj fishObj)
    {
        fishObj.hp -= atk;

        if(fishObj.hp <= 0)
        {
            fishObj.Dead(3);
        }
    }
}
