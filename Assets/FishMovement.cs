using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour {

    public float speed;
    public float fishSmoothSpeed;
    private float currentVelocity;
    public float addToTargetVal;

    private void Start()
    {
        StartCoroutine(RotatePeriodically());
    }

    void Update()
    {
        transform.position += transform.forward * speed;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, transform.eulerAngles.y + addToTargetVal, ref currentVelocity, fishSmoothSpeed);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
    }

    private IEnumerator RotatePeriodically()
    {
        while (true)
        {
            addToTargetVal = Random.Range(-180.0f, 180.0f);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
