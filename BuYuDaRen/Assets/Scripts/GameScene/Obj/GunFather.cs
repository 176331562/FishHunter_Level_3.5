using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFather : MonoBehaviour
{

    private GameObject nowGunObj;

    void Start()
    {
        CreateGun(0);
    }
 

    //创建炮
    public void CreateGun(int index)
    {
        if(nowGunObj != null)
        {
            Destroy(nowGunObj);
        }

        ShootGunData nowGunData = GameDataMgr.Instane.shootGunDatas[index];

        GameDataMgr.Instane.nowGunDataIndex = index;
        GameDataMgr.Instane.nowSelectGunData = nowGunData;

        nowGunObj = GameObject.Instantiate(Resources.Load<GameObject>("Gun/" + nowGunData.name), this.transform.position, Quaternion.identity);
        nowGunObj.transform.SetParent(this.transform, false);

       
        nowGunObj.AddComponent<ShootGun>();
    }

    //每按一次加就变到下一个等级的炮
    public void LevelUpGun()
    {
        int nowIndex = GameDataMgr.Instane.nowGunDataIndex;
        List<ShootGunData> shootGunDatas = GameDataMgr.Instane.shootGunDatas;

        nowIndex = nowIndex + 1 > shootGunDatas.Count - 1 ? 0 : nowIndex+1;

        Debug.LogError("当前下标为"+nowIndex);

        FireMusicMgr.Instance.PlayerAudio("ChangeFire");

        CreateGun(nowIndex);
    }

    //每按一次加就变到上一个等级的炮
    public void LevelDownGun()
    {
        int nowIndex = GameDataMgr.Instane.nowGunDataIndex;
        List<ShootGunData> shootGunDatas = GameDataMgr.Instane.shootGunDatas;

        nowIndex = nowIndex - 1 < 0? shootGunDatas.Count -1 : nowIndex-1;

        //Debug.LogError("当前下标为" + nowIndex);

        FireMusicMgr.Instance.PlayerAudio("ChangeFire");

        CreateGun(nowIndex);
    }
}
