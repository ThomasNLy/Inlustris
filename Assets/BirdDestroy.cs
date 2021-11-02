using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdDestroy : MonoBehaviour {


	void Start ()
    {
        StartCoroutine(DestroyBird());
	}

    IEnumerator DestroyBird()
    {
        yield return new WaitForSeconds(Random.Range(20.0f, 30.0f));
        Destroy(gameObject);
    }
}
