//挂在需要移动的物体上，如cube上
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stick_handler : MonoBehaviour {

    public Animator anim;

    public float mMoveSpeed = 2.0f;
    private joysticks mJoystick;
    // Use this for initialization
    void Start () {
        mJoystick = GameObject.Find("Joystick").GetComponent<joysticks>();        //找到joysticks组件
        mJoystick.JoystickMoveHandle = JoystickHandle;                          //注册手柄移动时的函数接口，手柄移动时会调用该函数接口
        mJoystick.JoystickEndHandle = JoystickEndHandle;

        anim = GetComponent<Animator>();
    }
	

    public void JoystickHandle(RectTransform content)
    {
        print("JoystickHandle");
        this.gameObject.transform.eulerAngles = new Vector3(0, -content.eulerAngles.z, 0);
        transform.Translate(Vector3.forward * Time.deltaTime * mMoveSpeed);
        anim.SetBool("isRun", true);
    }

    public void JoystickEndHandle(RectTransform rt)
    {
        anim.SetBool("isRun", false);
    }
}
