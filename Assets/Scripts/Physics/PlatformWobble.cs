using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWobble : MonoBehaviour {

    Vector3 floatToPosition;
    Vector3 positionRef;
    Vector3 currentVelocity;
    public float wobbleIntensity = 1.0f;
	// Use this for initialization
	void Start () {
        StartCoroutine("VectorChange");
        positionRef = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, positionRef + floatToPosition, ref currentVelocity, 2.5f * wobbleIntensity);
    }

    IEnumerator VectorChange()
    {
        while (true)
        {
            float randomX = Random.Range(-5f, 5f);
            float randomY = Random.Range(-5f, 5f);
            float randomZ = Random.Range(-5f, 5f);
            floatToPosition = new Vector3(randomX, randomY, randomZ);
            yield return new WaitForSeconds(5f);
        }
    }
}
