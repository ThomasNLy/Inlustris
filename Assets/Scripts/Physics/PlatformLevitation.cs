using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLevitation : MonoBehaviour {

    Vector3 originalPosition;
    Vector3 targetUpPosition;
    Vector3 targetDownPosition;
    Vector3 currentVelocity;
    public float min;
    public float max;

    // Use this for initialization
    void Start ()
    {
        targetUpPosition = transform.position + Vector3.up * Random.Range(min, max);
        targetDownPosition = transform.position + Vector3.down * Random.Range(min, max);
        StartCoroutine("LevitateUp");
    }

    IEnumerator LevitateUp()
    {
        while (Vector3.Distance(transform.position, targetUpPosition) > 5f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetUpPosition, ref currentVelocity, 2.0f);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine("LevitateDown");
    }

    IEnumerator LevitateDown()
    {
        while (Vector3.Distance(transform.position, targetDownPosition) > 5f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetDownPosition, ref currentVelocity, 2.0f);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine("LevitateUp");
    }
}
