using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarwhalMovement : MonoBehaviour {

    public float speed;
    public float xFishSmoothSpeed;
    public float yFishSmoothSpeed;
    public float zFishSmoothSpeed;
    private float xCurrentVelocity;
    private float yCurrentVelocity;
    private float zCurrentVelocity;
    private float targetXRot;
    private float targetYRot;
    private float targetZRot;

    private void Start()
    {
        StartCoroutine(RotatePeriodically());
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, targetXRot, ref xCurrentVelocity, xFishSmoothSpeed);
        float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetYRot, ref yCurrentVelocity, yFishSmoothSpeed);
        float zAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetZRot, ref zCurrentVelocity, zFishSmoothSpeed);
        transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);
    }

    private IEnumerator RotatePeriodically()
    {
        while (true)
        {
            targetXRot = Random.Range(-5.0f, 5.0f);
            targetYRot = Random.Range(-180.0f, 180.0f);
            targetZRot = Random.Range(-10.0f, 10.0f);
            yield return new WaitForSeconds(3.0f);
        }
    }
}
