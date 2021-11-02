using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawnPoints : MonoBehaviour {

    public GameObject starRef;
    public StarVertex thisStarVertex;

    float sphereRadius = 20.0f;

    void Start()
    {
        StartPoint();
    }

    void StartPoint()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(-300, 300), transform.position.z);
        if (Physics.CheckSphere(transform.position, sphereRadius))
        {
            StartPoint();
        }
        else
        {
            GameObject starCopy = Instantiate(starRef, transform.position, Quaternion.identity);
            starCopy.GetComponentInChildren<StarPickup>().thisStarVertex = thisStarVertex;
            starCopy.transform.parent = GameObject.FindGameObjectWithTag("StarsNotCollected").transform;
            Destroy(gameObject);
        }
    }
}
