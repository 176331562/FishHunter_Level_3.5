using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
public class JsonMgr
{
    private static JsonMgr instance = new JsonMgr();
    public static JsonMgr Instance => instance;

    private JsonMgr()
    {

    }

    public T Load<T>(string dataName) where T : new()
    {
        string path = Application.persistentDataPath + "/" + dataName + ".json";

        if (File.Exists(path))
        {
            if(string.IsNullOrEmpty(File.ReadAllText(path)))
            {
                T t = new T();

                File.WriteAllText(path, JsonMapper.ToJson(t));

                return t;
            }
            else
            {
                T t = JsonMapper.ToObject<T>(File.ReadAllText(path));

                return t;
            }
        }
        else
        {
            File.Create(path);

            T t = default(T);

            File.WriteAllText(path, JsonMapper.ToJson(t));

            return t;
        }
        
    }

    public void Save(object obj,string dataName)
    {
        string path = Application.persistentDataPath + "/" + dataName + ".json";

        if (File.Exists(path))
        {
            string contentText = JsonMapper.ToJson(obj);

            File.WriteAllText(path, contentText);
        }
        else
        {
            Debug.LogError("没有这个文件");
        }
    }
}
