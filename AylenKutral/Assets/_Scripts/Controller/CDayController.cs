using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDayController : MonoBehaviour {

    bool isDay;
    bool plusNumber;
    float speed = 15.0f;
    float dayNumber = 1;
    //public Transform targetPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Time.deltaTime * speed, 0, 0);
        //return;
        //transform.rotation.eulerAngles;
        //transform.RotateAround(Vector3.zero, Vector3.right, speed * Time.deltaTime);
        //transform.LookAt(Vector3.zero);


        //Debug.Log(totalRotation / 360.0f);
        if (transform.rotation.eulerAngles.x >= 0 && transform.rotation.eulerAngles.x <= 90)
        {
            speed = 15.0f;
            isDay = true;
        }
        if (transform.rotation.eulerAngles.x >= 270 && transform.rotation.eulerAngles.x <= 360)
        {
            speed = 30.0f;
            isDay = false;
            plusNumber = true;
        }
        if (transform.rotation.eulerAngles.x >= 359.5f && plusNumber)
        {
            plusNumber = false;
            dayNumber += 0.5f;
        }
//        Debug.Log(transform.rotation.eulerAngles.x);

        //if(!isDay && (int)(transform.rotation.eulerAngles.x /360.0f) == 1) dayNumber++;

       // Debug.Log("Is Day:" + isDay + ";" + "velocity:" + speed + ";" + "Day :" + (int)dayNumber);
    }
}
