using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObj : MonoBehaviour
{

    private static Transform coinFather;

    private void Start()
    {
        if(coinFather == null)
        {
            coinFather = GameObject.FindGameObjectWithTag("CoinTarget").transform;


        }
    }

    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, coinFather.position, 4 * Time.deltaTime);
    }
}
