using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour {

    public TriggerLights lightSwitch;
    public SpawnAssets spawnAssetsScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            if (lightSwitch != null)
                lightSwitch.GraduallyTurnOnLightsMethod();
            if (spawnAssetsScript != null)
                spawnAssetsScript.SpawnObjects();
            Destroy(this);
        }
    }
}
