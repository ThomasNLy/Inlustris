using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelugaMovement : MonoBehaviour {

    public float speed;
    public float xSmoothSpeed;
    public float ySmoothSpeed;
    private float xCurrentVelocity;
    private float yCurrentVelocity;
    private float randomXRot;
    private float randomYRot;


    private void Start()
    {
        StartCoroutine(RotatePeriodically());
    }

    void Update()
    {
        transform.position += transform.forward * speed;
        float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, randomXRot, ref xCurrentVelocity, xSmoothSpeed);
        float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, randomYRot, ref yCurrentVelocity, ySmoothSpeed);
        transform.eulerAngles = new Vector3(xAngle, yAngle, transform.eulerAngles.z);
    }

    private IEnumerator RotatePeriodically()
    {
        while (true)
        {
            randomXRot = Random.Range(-30.0f, 30.0f);
            randomYRot = Random.Range(-180.0f, 180.0f);
            yield return new WaitForSeconds(3.0f);
        }
    }
}
