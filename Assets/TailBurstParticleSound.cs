using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailBurstParticleSound : MonoBehaviour {

    AudioSource tailBurstSound; 

    private void Start()
    {
        tailBurstSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (!tailBurstSound.isPlaying)
            Destroy(gameObject);
	}
}
