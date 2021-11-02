using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarIndicator : MonoBehaviour {

    Vector3 currentVelocity;
    public float up;
    public float down;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("LevitateUp");
    }

    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 8, 0);
    }

    IEnumerator LevitateUp()
    {
        float counter = 0;
        while (counter < 1.0f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + transform.up * up, ref currentVelocity, 2.0f, 0.2f);
            counter += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine("LevitateDown");
    }

    IEnumerator LevitateDown()
    {
        float counter = 0;
        while (counter < 1.0f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, transform.position - transform.up * down, ref currentVelocity, 2.0f, 0.2f);
            counter += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);

        }
        StartCoroutine("LevitateUp");
    }
}
