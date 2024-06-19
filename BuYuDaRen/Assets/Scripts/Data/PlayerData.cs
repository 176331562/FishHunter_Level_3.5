using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string account;
    public int level;
    public int exp;
    public string levelName;
    public int gold;

}

public class PlayerInfo
{
    public List<PlayerData> playerDatas = new List<PlayerData>();
}
