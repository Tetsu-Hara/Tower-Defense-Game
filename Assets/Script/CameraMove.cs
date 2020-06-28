using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public float XZspeed = 18;
    public float Yspeed = 600;
    public float MouseSpeed = 3;

    Vector3 camRotain;
    public Camera useCamera;


    // Update is called once per frame
    void Update () {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Mouse ScrollWheel");

        transform.Translate(new Vector3(x*XZspeed, -y*Yspeed, z * XZspeed) * Time.deltaTime,Space.World);
	}
}
