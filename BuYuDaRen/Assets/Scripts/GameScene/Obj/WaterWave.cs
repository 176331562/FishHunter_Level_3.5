using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWave : MonoBehaviour
{

    private List<Texture> texture2DList;

    private Material material;

    private int nowIndex;

    void Start()
    {
        texture2DList = new List<Texture>();

        material = this.GetComponent<MeshRenderer>().material;

        Texture[] texture2Ds = Resources.LoadAll<Texture>("WaterWave");

        texture2DList.AddRange(texture2Ds);

        InvokeRepeating("ChangeTexture", 0, 0.07f);
    }

    private void ChangeTexture()
    {      
        nowIndex = nowIndex + 1 > texture2DList.Count - 1 ? 0 : nowIndex+1;

        material.mainTexture = texture2DList[nowIndex];
    }
}
