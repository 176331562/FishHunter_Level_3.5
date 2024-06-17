using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    public LoginInfo loginInfos;

    private static GameDataMgr instance => new GameDataMgr();
    public static GameDataMgr Instane => instance;

    private GameDataMgr()
    {
        loginInfos = JsonMgr.Instance.Load<LoginInfo>("LoginData");
    }

    public void SaveLoginData(LoginData loginData)
    {
        loginInfos.loginDatas.Add(loginData);

        JsonMgr.Instance.Save(loginInfos, "LoginData");
    }
}
