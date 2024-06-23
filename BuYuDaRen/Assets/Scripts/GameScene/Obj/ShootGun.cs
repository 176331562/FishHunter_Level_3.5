using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShootGun : MonoBehaviour
{
    [HideInInspector]
    public Transform UIFather;

    private float nowAngle;

    private List<GunBulletData> nowBulletData;

    private Transform shootPoint;

    void Start()
    {
        UIFather = GameObject.FindGameObjectWithTag("ShootFather").transform;

        nowBulletData = GameDataMgr.Instane.gunBulletDatas;

        shootPoint = this.transform.Find("Fire/FirePoint").transform;
    }


    void Update()
    {
        Vector2 pos;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(UIFather.transform as RectTransform, Input.mousePosition, Camera.main, out pos))
        {
            if(pos.x < this.transform.position.x)
            {
                nowAngle = Vector3.Angle(Vector3.up, pos - new Vector2(this.transform.position.x, this.transform.position.y));
            }
            else
            {
                nowAngle = -Vector3.Angle(Vector3.up, pos - new Vector2(this.transform.position.x, this.transform.position.y));
            }

            this.transform.rotation = Quaternion.AngleAxis(nowAngle, Vector3.forward);
        }

        //如果钱够了并且没点在UI上就发射炮弹
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
           
            if(GameDataMgr.Instane.nowSelectPlayerData.gold >= GameDataMgr.Instane.nowSelectGunData.gold)
            {
                

                GameDataMgr.Instane.nowSelectPlayerData.gold -= GameDataMgr.Instane.nowSelectGunData.gold;              

                CreateBullet(GameDataMgr.Instane.nowSelectPlayerData.level);

                UIManager.Instance.GetPanel<GamePanel>().ChangeGold(GameDataMgr.Instane.nowSelectPlayerData.gold);
            }          
        }
       
    }


    //创建子弹
   public void CreateBullet(int nowLevel)
    {       
        ShootGunData nowSelectGun = GameDataMgr.Instane.nowSelectGunData;

        int levelBullet = nowLevel % 10;
       
        //id*9
        int index = ((nowSelectGun.id-1) * (nowBulletData.Count / GameDataMgr.Instane.shootGunDatas.Count));

        

        for (int i = index; i < nowBulletData.Count; i++)
        {
            if(nowBulletData[i].level == levelBullet && nowSelectGun.name == nowBulletData[i].gunName)
            {
                GameDataMgr.Instane.nowSelectBulletData = nowBulletData[i];

                GameObject bulletObj = AssetBundleMgr.Instance.LoadAsset<GameObject>("bullets", nowBulletData[i].bulletName);

                //ResourceRequest rq = Resources.LoadAsync<GameObject>("Bullets/" + nowBulletData[i].bulletName);

                bulletObj = GameObject.Instantiate(bulletObj,
                    new Vector3(shootPoint.transform.position.x,shootPoint.transform.position.y,0),
                    shootPoint.transform.rotation);
                bulletObj.AddComponent<BulletObj>().InitBullet(GameDataMgr.Instane.webDatas[nowSelectGun.id-1]);

                FireMusicMgr.Instance.PlayerAudio("Fire");

                break;
            }
        }
    }
}
