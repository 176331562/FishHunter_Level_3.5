using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIManager
{
    private GameObject canvasObj;

    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private UIManager()
    {

    }

    public T ShowThisPanel<T>() where T : BasePanel
    {
        if(canvasObj == null)
        {
            canvasObj = GameObject.FindGameObjectWithTag("Canvas");

            if(canvasObj == null)
            {
                canvasObj = GameObject.Instantiate(AssetBundleMgr.Instance.LoadAsset<GameObject>("ui","Canvas"));
                //ResourceRequest rq1 = Resources.LoadAsync<GameObject>("UI/Canvas");

                //canvasObj = GameObject.Instantiate(rq1.asset as GameObject);

                //canvasObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
                Canvas canvas = canvasObj.GetComponent<Canvas>();
                canvas.worldCamera = Camera.main;
                canvasObj.name = "Canvas";
            }
            
        }

        string panelName = typeof(T).Name;

        if(panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }

        //GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        //ResourceRequest rq = Resources.LoadAsync<GameObject>("UI/" + panelName);
        //GameObject panelObj = GameObject.Instantiate(rq.asset as GameObject);
        GameObject panelObj = GameObject.Instantiate(AssetBundleMgr.Instance.LoadAsset<GameObject>("ui",panelName));


        panelObj.name = panelName;
        panelObj.transform.SetParent(canvasObj.transform,false);

        T panel = panelObj.GetComponent<T>();

        panelDic.Add(panelName, panel);
        panelDic[panelName].ShowThisPanel();
        return panelDic[panelName] as T;

    }

    public void CloseThisPanel<T>(bool isFade = false,UnityAction unityAction = null) where T:BasePanel
    {
        string panelName = typeof(T).Name;

        if (panelDic.ContainsKey(panelName))
        {
            if(isFade)
            {
                panelDic[panelName].CloseThisPanel(() =>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);

                    unityAction?.Invoke();
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);

                
            }
        }
        else
        {
            Debug.LogError("字典当前不存在当前UI面板");
        }
    }

    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if(panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }

        return null;
    }

    public void RemovePanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if(panelDic.ContainsKey(panelName))
        {
            panelDic.Remove(panelName);
        }
    }
}
