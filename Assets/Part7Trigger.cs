using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part7Trigger : SpawnAssets {

    public GameObject teleportcircle;
    public TriggerLights triggerLights;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            SpawnObjects();
            triggerLights.GraduallyTurnOnLightsMethod();
            teleportcircle.SetActive(true);
            //teleportcircle.GetComponent<ParticleSystem>().Play();
        }
    }
}
