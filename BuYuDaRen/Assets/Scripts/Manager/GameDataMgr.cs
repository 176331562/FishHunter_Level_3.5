using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    

    private static GameDataMgr instance;
    public static GameDataMgr Instane
    {
        get
        {
            if(instance == null)
            {
                instance = new GameDataMgr();
            }
            return instance;
        }
    }

    //当前正在使用的账号密码
    public LoginData nowSelectLogin;
    public int nowSelectIndex;
    public LoginInfo loginInfos;

    //当前音乐相关
    public MusicData musicData;

    //读取鱼的属性数据
    public List<FishData> fishDatas;

    //读取玩家仓库数据
    public PlayerInfo playerDatas;
    public PlayerData nowSelectPlayerData;
    public int nowPlayerIndex;

    private GameDataMgr()
    {
        loginInfos = JsonMgr.Instance.LoadData<LoginInfo>("LoginData");

        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");

        fishDatas = JsonMgr.Instance.LoadData<List<FishData>>("FishData");

        playerDatas = JsonMgr.Instance.LoadData<PlayerInfo>("PlayerData");

       
    }

    public void SaveLoginData(LoginData loginData)
    {
        //遍历用户数据列表
        for (int i = 0; i < loginInfos.loginDatas.Count; i++)
        {
            if(loginInfos.loginDatas[i].account == loginData.account)
            {
                //说明已经有这个用户
                loginInfos.loginDatas[i] = loginData;

                nowSelectLogin = loginData;
             
                JsonMgr.Instance.SaveData(loginInfos, "LoginData");

                return;
            }
        }

        loginInfos.loginDatas.Add(loginData);

        nowSelectLogin = loginData;

        JsonMgr.Instance.SaveData(loginInfos, "LoginData");
    }

    public void SaveMusicData(MusicData musicData)
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    public void SavePlayerData(PlayerData playerData)
    {
        //说明当前打开的是当前登录的仓库数据
        if(nowSelectLogin.account == playerData.account)
        {
            for (int i = 0; i < playerDatas.playerDatas.Count; i++)
            {
                //如果里面有这个用户数据，就直接替换
                if(playerDatas.playerDatas[i].account == playerData.account)
                {
                    playerDatas.playerDatas[i] = playerData;

                    JsonMgr.Instance.SaveData(playerDatas, "PlayerData");

                    return;
                }
               
            }

            playerDatas.playerDatas.Add(playerData);
            JsonMgr.Instance.SaveData(playerDatas, "PlayerData");

        }
    }

    public void InitNowSelectLogin(LoginData loginData,int nowSelectLoginIndex)
    {
        this.nowSelectLogin = loginData;

        this.nowSelectIndex = nowSelectLoginIndex;

        
    }

    public PlayerData GetNowPlayerData()
    {
        Debug.LogError(nowSelectLogin.account);

        for (int i = 0; i < playerDatas.playerDatas.Count; i++)
        {
            //如果里面有这个用户数据，就直接替换
            if (playerDatas.playerDatas[i].account == nowSelectLogin.account)
            {
                nowSelectPlayerData = playerDatas.playerDatas[i];
                nowPlayerIndex = i;
                return nowSelectPlayerData;
            }
        }

        PlayerData playerData = new PlayerData();
        playerData.account = nowSelectLogin.account;
        playerData.level = 1;
        playerData.levelName = "菜鸟";
        playerData.gold = 500;
        playerData.exp = 0;

        JsonMgr.Instance.SaveData(playerData, "PlayerData");

        return playerData;
    }
}
