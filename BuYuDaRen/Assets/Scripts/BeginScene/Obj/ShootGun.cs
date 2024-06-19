using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShootGun : MonoBehaviour
{
    private Transform UIFather;

    private float nowAngle;

    void Start()
    {
        UIFather = this.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(UIFather.transform as RectTransform, Input.mousePosition, Camera.main, out pos))
        {
            if(pos.x < this.transform.position.x)
            {
                nowAngle = Vector3.Angle(Vector3.up, pos - new Vector2(this.transform.position.x, this.transform.position.y));
            }
            else
            {
                nowAngle = -Vector3.Angle(Vector3.up, pos - new Vector2(this.transform.position.x, this.transform.position.y));
            }

            this.transform.rotation = Quaternion.AngleAxis(nowAngle, Vector3.forward);
        }

        
    }
}
