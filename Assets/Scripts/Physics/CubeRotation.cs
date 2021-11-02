using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{

    public float dstFromTarget;
    public Transform target; //the target the camera follows, initialize it in other scripts

    public float yawSpeed;
    public float rotationSmoothTime;
    Vector3 rotationSmoothVel;
    Vector3 currentRotation;

    float yaw; //horizontal camera movement

    // Use this for initialization
    void Start()
    {
        yawSpeed = 1f;
        dstFromTarget = 3;
        rotationSmoothTime = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(0, yawSpeed), ref rotationSmoothVel, rotationSmoothTime);
        transform.eulerAngles += currentRotation;
        transform.position = target.position - transform.forward * dstFromTarget;
    }
}
