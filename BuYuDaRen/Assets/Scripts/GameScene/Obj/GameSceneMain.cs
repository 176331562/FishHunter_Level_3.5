using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneMain : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
      
        PlayerData playerData = GameDataMgr.Instane.GetNowPlayerData();       

        UIManager.Instance.ShowThisPanel<GamePanel>().InitPanel(playerData);
    }

    private void OnDisable()
    {
        UIManager.Instance.panelObj = null;

        UIManager.Instance.RemovePanel<GamePanel>();
    }
}
