using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishObj : MonoBehaviour
{

    private Animator animator;

    private SpriteRenderer spriteRenderer;


    public int id;
    public int hp;
    public int exp;
    public float speed;
    public int gold;

    private bool isDead = false;

    private void OnEnable()
    {
        animator = this.GetComponent<Animator>();

        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!isDead)
        {
            this.transform.Translate(Vector3.right * 3 * Time.deltaTime);
        }        
    }

    public void InitFish(int createIndex,FishData fishData)
    {
        this.hp = fishData.hp;
        this.id = fishData.id;
        this.exp = fishData.exp;
        this.speed = fishData.speed;
        this.gold = fishData.gold;

        spriteRenderer.sortingOrder = fishData.layer+createIndex;
    }

    public void Dead(float time)
    {

        isDead = true;
        animator.SetTrigger("Dead");

        this.GetComponent<BoxCollider>().enabled = false;

        GameDataMgr.Instane.nowSelectPlayerData.exp += exp;
        GameDataMgr.Instane.nowSelectPlayerData.gold += gold;

        UIManager.Instance.GetPanel<GamePanel>().ChangeGold(GameDataMgr.Instane.nowSelectPlayerData.gold);
        UIManager.Instance.GetPanel<GamePanel>().ChangeLevel(GameDataMgr.Instane.nowSelectPlayerData.exp,
            GameDataMgr.Instane.nowSelectPlayerData.level);


        Destroy(this.gameObject, time);
    }
}
