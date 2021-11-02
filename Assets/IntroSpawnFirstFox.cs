using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSpawnFirstFox : MonoBehaviour {

    public GameObject foxSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            Instantiate(foxSpawn);
            Instantiate(foxSpawn);
        }
    }
}
