using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginSceneMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.ShowThisPanel<LoginPanel>();
     
        //Debug.LogError(Application.persistentDataPath);
    }

    
}
