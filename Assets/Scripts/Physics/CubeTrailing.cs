using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrailing : MonoBehaviour {

    public float dstFromTarget;
    public static Transform target; //the target the camera follows
    public static Transform follower;
    public Transform targetRef;
    public Transform followerRef;

    public static float turnSmoothIncrement;
    public float turnSmoothTime;
    float turnSmoothVelocity;

    Transform cameraTransform;

    // Use this for initialization
    void Start()
    {
        cameraTransform = Camera.main.transform;

        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("PlayerTail").transform;
            follower = gameObject.transform;
        }
        else
        {
            target = follower;
            follower = gameObject.transform;
        }
        targetRef = target;
        followerRef = follower;
  
        dstFromTarget = 3;
        turnSmoothIncrement += 0.25f;
        turnSmoothTime = turnSmoothIncrement;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //horizontal input(A,D) vertical input(W,S)
        Vector2 inputDir = input.normalized; //convert to unit vector

        float targetRotation2 = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y; //angle formed by the two input variables
        float yRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation2, ref turnSmoothVelocity, turnSmoothTime);

        transform.eulerAngles = new Vector3(cameraTransform.eulerAngles.x, yRotation, 0); //turn FROM player's current y-angle TO the target's y-angle

        transform.position = targetRef.position - transform.forward * dstFromTarget;
    }
}
