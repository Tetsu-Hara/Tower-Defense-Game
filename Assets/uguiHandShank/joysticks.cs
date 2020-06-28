//挂在image1上面，设置image的joysticks组件的content为Image2的recttransform
/// https://blog.csdn.net/qq_33747722/article/details/79971527
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class joysticks : ScrollRect
{

    private float mRadius;  //半径
    public System.Action<RectTransform> JoystickMoveHandle;
    public System.Action<RectTransform> JoystickEndHandle;


    protected override void Start()    //override重写
    {
        mRadius = this.GetComponent<RectTransform>().sizeDelta.x * 0.5f;  //获得半径；sizeDelta: rect的宽高
        this.content.gameObject.SetActive(false);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        this.content.gameObject.SetActive(true);

        //虚拟摇杆移动
        var contentPostion = this.content.anchoredPosition;
        if (contentPostion.magnitude > mRadius)
        {
            contentPostion = contentPostion.normalized * mRadius;
            SetContentAnchoredPosition(contentPostion);
        }
        //旋转
        if (content.anchoredPosition.y != 0)
        {
            content.eulerAngles = new Vector3(0, 0, Vector3.Angle(Vector3.right, content.anchoredPosition) * content.anchoredPosition.y / Mathf.Abs(content.anchoredPosition.y) - 90);
        }

    }

    private void FixedUpdate()
    {
        //当content激活时，调用注册的函数接口 JoystickMoveHandle
        if (this.content.gameObject.activeInHierarchy)
        {
            if (JoystickMoveHandle != null)
            {
                JoystickMoveHandle(this.content);
            }
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        this.content.gameObject.SetActive(false);

        if (JoystickEndHandle != null)
        {
            JoystickEndHandle(this.content);
        }
    }

}

