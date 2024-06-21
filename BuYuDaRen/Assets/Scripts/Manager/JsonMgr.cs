using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
public enum JsonType
{
    JsonUtlity,
    LitJson,
}


public class JsonMgr
{
    private static JsonMgr instance = new JsonMgr();
    public static JsonMgr Instance => instance;

    private JsonMgr()
    {

    }

    public T LoadData<T>(string dataName) where T : new()
    {
        string path = Application.streamingAssetsPath + "/Data/" + dataName + ".json";

        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + dataName + ".json";

        if (!File.Exists(path))
            return new T();

        string contentText = string.Empty;        

        contentText = File.ReadAllText(path);

        T t = JsonMapper.ToObject<T>(contentText);

        return t;
    }

    public void SaveData(object obj,string dataName)
    {
        string path = Application.persistentDataPath + "/" + dataName + ".json";

        string contentText = string.Empty;

        contentText = JsonMapper.ToJson(obj);

        File.WriteAllText(path, contentText);
    }
}
   
