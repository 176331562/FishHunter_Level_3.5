using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{

    private WebData webData;

    private void Start()
    {
        
    }

    void Update()
    {
        this.transform.Translate(Vector3.up * 5 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fish"))
        {
            CreateWeb(other.GetComponent<SpriteRenderer>().sortingOrder,3,other.gameObject);

            Destroy(this.gameObject);
        }
    }

    public void InitBullet(WebData webData)
    {
        this.webData = webData;
    }

    private void CreateWeb(int layer,float time,GameObject fishObj)
    {
        ResourceRequest rq = Resources.LoadAsync<GameObject>("Web/" + webData.name);

        GameObject webObj = GameObject.Instantiate(rq.asset as GameObject, this.transform.position, this.transform.rotation);

        WebObj web = webObj.AddComponent<WebObj>();

        web.InitWeb(layer,webData);

        web.HurtFish(fishObj.GetComponent<FishObj>());

        web.DelayDestroy(time);
    }

    
}
