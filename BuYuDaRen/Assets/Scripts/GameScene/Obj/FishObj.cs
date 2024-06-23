using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishObj : MonoBehaviour
{

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    public bool isLine;

    public int id;
    public int hp;
    public int exp;
    public float speed;
    public int gold;

    private bool isDead = false;

    private FishData nowFishData;

    //随机每一条鱼的旋转速度
    private float rotateSpeed;

    private void OnEnable()
    {
        animator = this.GetComponent<Animator>();

        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!isDead && isLine)
        {
            this.transform.Translate(Vector3.right * 3 * Time.deltaTime);
        }

        if(!isLine && !isDead)
        {
            this.transform.Translate(Vector3.right * 3 * Time.deltaTime);
            this.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
    }

    public void InitFish(int createIndex,FishData fishData,bool isLine,float rotateSpeed)
    {
        this.hp = fishData.hp;
        this.id = fishData.id;
        this.exp = fishData.exp;
        this.speed = fishData.speed;
        this.gold = fishData.gold;
        this.isLine = isLine;
        this.nowFishData = fishData;
        this.rotateSpeed = rotateSpeed;

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

        FishMusicMgr.Instance.PlayerAudio(nowFishData.sound);
        

        if(nowFishData.coin != "null")
        {       
            GameObject otherObj = AssetBundleMgr.Instance.LoadAsset<GameObject>("coin", nowFishData.coin);
            GameObject coinObj = GameObject.Instantiate(otherObj, this.transform.position, this.transform.rotation);
        }

        if (nowFishData.effect != "null")
        {
            GameObject otherObj = AssetBundleMgr.Instance.LoadAsset<GameObject>("effect", nowFishData.effect);
            GameObject effectObj = GameObject.Instantiate(otherObj, this.transform.position, this.transform.rotation);
        }

        Destroy(this.gameObject, time);
    }
}
