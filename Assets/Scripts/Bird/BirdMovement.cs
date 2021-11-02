using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour {

    public float speed;
    public float currentVelocity;
	// Update is called once per frame
	void Update () {

        transform.position += transform.forward * speed;
        float addToTargetVal = Random.Range(-180.0f, 180.0f);
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, transform.eulerAngles.y + addToTargetVal, ref currentVelocity, 0.7f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
	}
}
