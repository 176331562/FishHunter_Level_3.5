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
    public List<PlayerData> playerDatas;
    public PlayerData nowSelectPlayerData;
    public int nowPlayerIndex;


    //读取枪械数据
    public List<ShootGunData> shootGunDatas;
    public ShootGunData nowSelectGunData;
    public int nowGunDataIndex;


    //读取子弹数据
    public List<GunBulletData> gunBulletDatas;
    public GunBulletData nowSelectBulletData;

    //读取捕鱼网数据
    public List<WebData> webDatas;


    //读取等级称号数据
    public List<LevelNameData> levelNameDatas;
    public LevelNameData nowlevelNameData;
    public int nowLevelIndex;

    //当前场景中存在多少个生成点
    public Dictionary<string,FishPoint> fishPoints = new Dictionary<string, FishPoint>();
    
    //当前背景下标
    public int nowBKIndex;


    private GameDataMgr()
    {
        loginInfos = JsonMgr.Instance.LoadData<LoginInfo>("LoginData");

        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");

        fishDatas = JsonMgr.Instance.LoadData<List<FishData>>("FishData");

        playerDatas = JsonMgr.Instance.LoadData<List<PlayerData>>("PlayerData");

        shootGunDatas = JsonMgr.Instance.LoadData<List<ShootGunData>>("ShootGunData");

        gunBulletDatas = JsonMgr.Instance.LoadData<List<GunBulletData>>("GunBulletData");

        webDatas = JsonMgr.Instance.LoadData<List<WebData>>("WebData");

        levelNameDatas = JsonMgr.Instance.LoadData<List<LevelNameData>>("LevelNameData");

        
    }

    public void SaveLoginData(LoginData loginData)
    {

        if(loginInfos == null)
        {
            loginInfos.loginDatas.Add(loginData);

            nowSelectLogin = loginData;

            JsonMgr.Instance.SaveData(loginInfos, "LoginData");

            return;
        }

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
        if (nowSelectLogin.account == playerData.account)
        {           
            for (int i = 0; i < playerDatas.Count; i++)
            {
                //如果里面有这个用户数据，就直接替换
                if(playerDatas[i].account == playerData.account)
                {
                    playerDatas[i] = playerData;
                   
                    JsonMgr.Instance.SaveData(playerDatas, "PlayerData");

                    return;
                }              
            }

            playerDatas.Add(playerData);

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
        for (int i = 0; i < playerDatas.Count; i++)
        {
            //如果里面有这个用户数据，就直接替换
            if (playerDatas[i].account == nowSelectLogin.account)
            {
                nowSelectPlayerData = playerDatas[i];
                nowPlayerIndex = i;

                return nowSelectPlayerData;
            }
        }

        PlayerData playerData = new PlayerData();
        playerData.account = nowSelectLogin.account;
        //playerData.account = "2";
        playerData.level = 1;
        playerData.levelName = levelNameDatas[0].levelName;
        playerData.gold = 50000;
        playerData.exp = 0;

        playerDatas.Add(playerData);
        nowSelectPlayerData = playerData;

        JsonMgr.Instance.SaveData(playerDatas, "PlayerData");

        return playerData;
    }

    
}
