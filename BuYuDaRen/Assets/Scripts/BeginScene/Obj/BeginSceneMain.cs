using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginSceneMain : MonoBehaviour
{

    private void Awake()
    {
        UIManager.Instance.ShowThisPanel<LoginPanel>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
     
        //Debug.LogError(Application.persistentDataPath);
    }

    
}
