using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    

    private static GameDataMgr instance => new GameDataMgr();
    public static GameDataMgr Instane => instance;

    //当前正在使用的账号密码
    public LoginData nowSelectLogin;
    public int nowSelectIndex;
    public LoginInfo loginInfos;

    //当前音乐相关
    public MusicData musicData;

    private GameDataMgr()
    {
        loginInfos = JsonMgr.Instance.LoadData<LoginInfo>("LoginData");

        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
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

                JsonMgr.Instance.SaveData(loginInfos, "LoginData");

                return;
            }
        }

        loginInfos.loginDatas.Add(loginData);

        JsonMgr.Instance.SaveData(loginInfos, "LoginData");
    }

    public void SaveMusicData(MusicData musicData)
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    //public void SetMusicIsOpen(bool b)
    //{

    //}
}
