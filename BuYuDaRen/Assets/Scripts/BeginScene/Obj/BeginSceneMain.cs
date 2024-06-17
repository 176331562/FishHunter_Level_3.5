using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginSceneMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.ShowThisPanel<BeginPanel>();

        LoginData loginData = new LoginData();
        loginData.account = "fsc";
        loginData.password = "123";


        GameDataMgr.Instane.SaveLoginData(loginData);

        Debug.LogError(Application.persistentDataPath);
    }

    
}
