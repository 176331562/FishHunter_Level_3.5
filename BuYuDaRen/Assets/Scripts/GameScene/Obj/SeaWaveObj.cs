using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWaveObj : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(-Vector3.right * 10 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fish"))
        {
            Destroy(other.gameObject);
        }

        if(other.CompareTag("AirWall"))
        {
            Destroy(this.gameObject);
        }
    }
}
