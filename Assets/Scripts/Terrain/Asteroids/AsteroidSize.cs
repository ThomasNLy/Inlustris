using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSize : MonoBehaviour {

    public float randomMinScale;
    public float randomMaxScale;

    // Use this for initialization
    void Start () {
        transform.localScale = transform.localScale * Random.Range(randomMinScale, randomMaxScale);
    }
}
