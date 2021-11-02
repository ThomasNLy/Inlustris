using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotate : MonoBehaviour {

    public float rotateXSpeed;
    public float rotateYSpeed;
    public float rotateZSpeed;

    // Update is called once per frame
    void Update () {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + rotateXSpeed, transform.eulerAngles.y + rotateYSpeed, transform.eulerAngles.z + rotateZSpeed);
	}
}
