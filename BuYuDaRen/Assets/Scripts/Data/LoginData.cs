using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginData
{
    public string account;
    public string password;

    public bool isRememberAccount;
    public bool isRememberPassWord;

    public string tips;
}

public class LoginInfo
{
    public List<LoginData> loginDatas = new List<LoginData>();
}
