using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    public float dstFromTarget;
    private float currentDstFromTarget;
    private float newDstFromTarget;
    public float smoothSpeed;
    private float currentVelocity;
    public float rotateSpeed;
    public Transform target;

    private void Start()
    {
        currentDstFromTarget = dstFromTarget;
        StartCoroutine(ChangeDistance());
    }

    public virtual void Update()
    {
        currentDstFromTarget = Mathf.SmoothDamp(currentDstFromTarget, newDstFromTarget, ref currentVelocity, smoothSpeed);
        transform.position = target.position - transform.forward * currentDstFromTarget;
        transform.RotateAround(target.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }

    IEnumerator ChangeDistance()
    {
        while (true)
        {
            newDstFromTarget = Random.Range(50.0f, dstFromTarget);
            yield return new WaitForSeconds(3.0f);
        }
    }
}
