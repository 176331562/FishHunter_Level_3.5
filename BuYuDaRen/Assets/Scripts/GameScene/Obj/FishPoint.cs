using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPoint : MonoBehaviour
{

    private float delayTime = 3;

   
    void Start()
    {
        CreateFish();
    }

   
    public void CreateFish()
    {
        int num = Random.Range(1, 10);
        int fishIndex = Random.Range(0, 17);

        FishData fishData = GameDataMgr.Instane.fishDatas[fishIndex];

        StartCoroutine(DelayTimeToCreate(fishData, num, delayTime));
    }

    IEnumerator DelayTimeToCreate(FishData fishData,int createNum,float time)
    {
        int nowCreateIndex = 0;

        ResourceRequest rq = Resources.LoadAsync<GameObject>("Fish/" + fishData.name);

        while (nowCreateIndex < createNum)
        {
            GameObject fishObj = GameObject.Instantiate(rq.asset as GameObject, this.transform.position, this.transform.rotation);
            fishObj.name = fishData.name;
            fishObj.transform.SetParent(this.transform);
            FishObj fishObjComponet = fishObj.AddComponent<FishObj>();
            fishObjComponet.InitFish(nowCreateIndex,fishData);

            ++nowCreateIndex;

            yield return new WaitForSeconds(delayTime);
        }
    }
}
