using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour {

    public int numberOfAsteroids;
    public float asteroidDistanceFromSpawn;
    public float asteroidRandomRotMinSpeed;
    public float asteroidRandomRotMaxSpeed;
    public float asteroidRandomMinScale;
    public float asteroidRandomMaxScale;
    public bool addColliders;
    // Use this for initialization
    void Start () {
        int count = 0;
        while (count < numberOfAsteroids)
        {
            Vector3 randomPos = new Vector3(transform.position.x + Random.Range(-10.0f, 10.0f), transform.position.y + Random.Range(-5.0f, 5.0f), transform.position.z + Random.Range(-10.0f, 10.0f));
            GameObject asteroidRef = Instantiate((GameObject)Resources.Load("prefabs/level1/obstacles/asteroid", typeof(GameObject)), randomPos, Quaternion.Euler(Random.Range(-360.0f, 360.0f), Random.Range(-360.0f, 360.0f), Random.Range(-360.0f, 360.0f)));
            asteroidRef.GetComponent<AsteroidRotateAround>().target = transform;
            asteroidRef.transform.parent = transform;
            asteroidRef.GetComponent<AsteroidRotateAround>().dstFromTarget = asteroidDistanceFromSpawn;
            asteroidRef.GetComponent<AsteroidRotateAround>().rotateSpeed = Random.Range(asteroidRandomRotMinSpeed, asteroidRandomRotMaxSpeed);
            asteroidRef.GetComponent<AsteroidRotateAround>().smoothSpeed = 5.0f;
            asteroidRef.GetComponent<AsteroidSize>().randomMinScale = asteroidRandomMinScale;
            asteroidRef.GetComponent<AsteroidSize>().randomMaxScale = asteroidRandomMaxScale;
            if (addColliders)
                asteroidRef.AddComponent<MeshCollider>();
            count++;
        }
	}
}
