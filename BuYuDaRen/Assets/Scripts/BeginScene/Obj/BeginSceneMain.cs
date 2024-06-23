using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginSceneMain : MonoBehaviour
{

    private void Awake()
    {
        UIManager.Instance.ShowThisPanel<LoginPanel>();
    }  
}
