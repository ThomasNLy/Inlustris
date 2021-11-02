using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour {

    private bool isGrounded;
    private float enterTime;
    private float stayTime;
    private float delayAfterGrounded;

    private void Start()
    {
        delayAfterGrounded = 0.1f;
    }

    public bool IsGrounded
    {
        get { return isGrounded; }
        set { isGrounded = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerGroundDetect")) {
            enterTime = Time.time;
            Debug.Log("Enter: " + enterTime);
            IsGrounded = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerGroundDetect") && !IsGrounded)
        {
            stayTime = Time.time - enterTime;
            Debug.Log("Stay: " + stayTime);
            IsGrounded = false;
        }

        if (stayTime > delayAfterGrounded)
            IsGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerGroundDetect"))
        {
            stayTime = 0;
            enterTime = 0;
        }
    }
}
