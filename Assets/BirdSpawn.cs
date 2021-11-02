using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawn : MonoBehaviour {

    public GameObject birdPrefab;
    public int numberOfBirds;
    public float birdRandomMinScale;
    public float birdRandomMaxScale;
    public float birdRandomMinPivotY;
    public float birdRandomMaxPivotY;
    // Use this for initialization
    void Start()
    {
        int count = 0;
        while (count < numberOfBirds)
        {
            Vector3 randomPos = new Vector3(transform.position.x + Random.Range(-10.0f, 10.0f), transform.position.y + Random.Range(-5.0f, 5.0f), transform.position.z + Random.Range(-10.0f, 10.0f));
            GameObject birdRef = Instantiate(birdPrefab, randomPos, Quaternion.Euler(Random.Range(-360.0f, 360.0f), Random.Range(-360.0f, 360.0f), Random.Range(-360.0f, 360.0f)));
            birdRef.GetComponent<BirdMovement>().speed = Random.Range(0.05f, 0.2f);
            float randomScaleVal = Random.Range(birdRandomMinScale, birdRandomMaxScale);
            Vector3 randomScaleVector = new Vector3 (randomScaleVal, randomScaleVal, randomScaleVal);
            birdRef.transform.localScale = randomScaleVector;
            birdRef.transform.position += new Vector3(0, Random.Range(birdRandomMinPivotY, birdRandomMaxPivotY), 0);
            count++;
        }
    }
}
