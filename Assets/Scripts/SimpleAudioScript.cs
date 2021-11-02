using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleAudioScript : MonoBehaviour {
    public GameObject soundSourcesObj;
    public AudioSource[] soundSources;

    // Use this for initialization
    void Start() {
        soundSources = soundSourcesObj.GetComponents<AudioSource>();
        StartCoroutine(RandomPlayTime());
    }

    public IEnumerator RandomPlayTime()
    {
        while (true)
        {
            GetComponent<AudioSource>().clip = soundSources[Random.Range(0, soundSources.Length)].clip;
            yield return new WaitForSeconds(Random.Range(0.0f, 3.0f));
            GetComponent<AudioSource>().Play();
        }
    }
}
