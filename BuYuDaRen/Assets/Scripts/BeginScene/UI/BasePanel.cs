using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public abstract class BasePanel : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    private float canvasSpeed = 10;

    private bool isShow = false;

    private UnityAction callBack;

    protected virtual void Awake()
    {
        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
    }

    protected virtual void Start()
    {
        

        Init();
    }


    protected virtual void Update()
    {
        if(isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += canvasSpeed * Time.deltaTime;

            if(canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        else if(!isShow && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= canvasSpeed * Time.deltaTime;

            if(canvasGroup.alpha <= 0.1f)
            {
                canvasGroup.alpha = 0;

                callBack?.Invoke();
            }
        }
    }

    public abstract void Init();

    public virtual void ShowThisPanel()
    {
        canvasGroup.alpha = 0;

        isShow = true;
    }

    public virtual void CloseThisPanel(UnityAction unityAction = null)
    {
        canvasGroup.alpha = 1;

        callBack = unityAction;

        isShow = false;
    }
}
