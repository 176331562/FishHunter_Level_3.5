using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPointFather : MonoBehaviour
{

    Dictionary<string, FishPoint> fishPoints;

    private void AddFishPoint()
    {
        //先看看是不是已经存储过了
        if (fishPoints.Count != this.transform.childCount)
        {
            //添加子物体
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (!fishPoints.ContainsKey(this.transform.GetChild(i).name))
                {
                    FishPoint fishPoint = this.transform.GetChild(i).GetComponent<FishPoint>();
                    fishPoint.InitState(true);

                    fishPoints.Add(fishPoint.gameObject.name, fishPoint);
                }
            }

            GameDataMgr.Instane.fishPoints = fishPoints;
        }
    }


    private void ShowFishPointToCreate(float delayTime)
    {
        foreach (FishPoint item in GameDataMgr.Instane.fishPoints.Values)
        {          
            if (item.isFinishCreate)
            {               
                item.CreateFish(delayTime);
            }
        }      
    }

    IEnumerator CreateFish(float delayTime)
    {
        while(true)
        {
            ShowFishPointToCreate(delayTime);

            yield return new WaitForSeconds(delayTime);
        }
    }

    private void OnEnable()
    {
        fishPoints = new Dictionary<string, FishPoint>();

        ResetFishPoint();

        AddFishPoint();

        StartCoroutine(CreateFish(2));
    }

    private void OnDisable()
    {
        StopCoroutine("CreateFish");
    }

    //初始化鱼的生成点的状态
    private void ResetFishPoint()
    {
        foreach (FishPoint item in fishPoints.Values)
        {
            item.isFinishCreate = true;
        }
    }
}
