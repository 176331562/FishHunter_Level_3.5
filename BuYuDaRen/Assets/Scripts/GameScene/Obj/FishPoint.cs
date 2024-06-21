using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPoint : MonoBehaviour
{
    
    public bool isFinishCreate;

  
    public void CreateFish(float delayTime)
    {        
        //防止鱼数量过多
        int num = Random.Range(1, 3);

        //生成鱼的种类
        int fishIndex = Random.Range(0, 17);

        //此条鱼是直线还是拐弯
        bool isLine = Random.Range(0, 2) == 0 ? true : false;

        //随机旋转速度
        float rotateSpeed = Random.Range(-10, 11);

        FishData fishData = GameDataMgr.Instane.fishDatas[fishIndex];

        StartCoroutine(DelayTimeToCreate(fishData, num, delayTime,isLine,rotateSpeed,delayTime));

       
    }

    //延迟去生成鱼
    IEnumerator DelayTimeToCreate(FishData fishData,int createNum,float time,bool isLine,float rotateSpeed,float delayTime)
    {
        //重置当前生成状态
        isFinishCreate = false;

        int nowCreateIndex = 0;

        ResourceRequest rq = Resources.LoadAsync<GameObject>("Fish/" + fishData.name);

        while (nowCreateIndex < createNum)
        {
            GameObject fishObj = GameObject.Instantiate(rq.asset as GameObject, this.transform.position, this.transform.rotation);
            fishObj.name = fishData.name;
            fishObj.transform.SetParent(this.transform);
            FishObj fishObjComponet = fishObj.AddComponent<FishObj>();
            fishObjComponet.InitFish(nowCreateIndex,fishData,isLine,rotateSpeed);

            ++nowCreateIndex;

            yield return new WaitForSeconds(delayTime);
        }

        isFinishCreate = true;
    }

    public void InitState(bool isFinish)
    {
        this.isFinishCreate = isFinish;
    }
}
