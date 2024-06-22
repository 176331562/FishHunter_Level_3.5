using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError(Application.persistentDataPath);

        PlayerData playerData = GameDataMgr.Instane.GetNowPlayerData();       

        UIManager.Instance.ShowThisPanel<GamePanel>().InitPanel(playerData);
    }

  
}
