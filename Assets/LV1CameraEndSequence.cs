using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV1CameraEndSequence : MonoBehaviour {

    public float dstFromTarget;
    public float currentDstFromTargetVel;

    public float rotateYSpeed;
    public float currentRotationYVel;

    public float rotateXSpeed;
    public float currentRotationXVel;

    public Transform target; //the target the camera follows
    public bool activated;
    public bool phoenixSummoned;

    void Update()
    {
        if (activated)
        {
            if (!phoenixSummoned)
            {
                dstFromTarget = Mathf.SmoothDamp(dstFromTarget, 1200.0f, ref currentDstFromTargetVel, 10.0f);
                transform.position = target.position - transform.forward * dstFromTarget;

                rotateYSpeed = Mathf.SmoothDamp(rotateYSpeed, 0.0f, ref currentRotationYVel, 10.0f);
                transform.RotateAround(target.transform.position, Vector3.up, rotateYSpeed * Time.deltaTime);
            }

            else
            {
                dstFromTarget = Mathf.SmoothDamp(dstFromTarget, 800.0f, ref currentDstFromTargetVel, 10.0f);
                transform.position = target.position - transform.forward * dstFromTarget;

                rotateYSpeed = Mathf.SmoothDamp(rotateYSpeed, 0.0f, ref currentRotationYVel, 10.0f);
                transform.RotateAround(target.transform.position, Vector3.up, rotateYSpeed * Time.deltaTime);

                rotateXSpeed = transform.eulerAngles.x;
                rotateXSpeed = Mathf.SmoothDampAngle(rotateXSpeed, 90.0f, ref currentRotationXVel, 5.0f);
                transform.eulerAngles = new Vector3(rotateXSpeed, transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }
    }
}
