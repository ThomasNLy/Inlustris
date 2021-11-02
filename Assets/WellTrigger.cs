using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellTrigger : MonoBehaviour {

    public GameObject waterFallRef;
    public GameObject[] fishes;
    public Material fishMaterial;
    public TriggerLights lightSwitch;
    public GameObject associatedRock;

    private void Awake()
    {
        waterFallRef.GetComponent<ParticleSystem>().Stop();
        waterFallRef.GetComponent<AudioSource>().Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(associatedRock))
        {
            foreach (GameObject fish in fishes)
            {
                fish.GetComponentInChildren<SkinnedMeshRenderer>().material = fishMaterial;
                fish.GetComponent<Animator>().SetBool("wellActivated", true);
                fish.AddComponent<FishMovement>().speed = 0.005f;
                fish.GetComponent<FishMovement>().fishSmoothSpeed = 0.6f;
            }
            lightSwitch.GraduallyTurnOnLightsMethod();
            waterFallRef.GetComponent<ParticleSystem>().Play();
            waterFallRef.GetComponent<AudioSource>().Play();
            Destroy(this);
        }
    }
}
