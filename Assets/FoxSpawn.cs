using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxSpawn : MonoBehaviour {

    public GameObject foxIdle;
	// Use this for initialization
	void Start () {
        StartCoroutine("SpawnFoxDelay");
	}

    IEnumerator SpawnFoxDelay()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
        StartCoroutine("SpawnFox");
    }

    IEnumerator SpawnFox()
    {
        Transform playerTF = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 randomPosAroundPlayer = playerTF.position + playerTF.forward * 350.0f + playerTF.right * (Random.Range(-300.0f, 300.0f));
        GameObject foxIdleCopy = Instantiate(foxIdle, randomPosAroundPlayer, Quaternion.Euler(0, Random.Range(-360, 360), 0));
        Destroy(gameObject);
        yield return null;
    }
}
