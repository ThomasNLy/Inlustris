using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixTreeGlow : Glow {

    MeshRenderer[] leavesMaterial;
    public TreePlayerRay chargingRay;
    public AudioSource glowSound;
	// Use this for initialization
	void Start ()
    {
        leavesMaterial = GetComponentsInChildren<MeshRenderer>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            glowSound.Play();
            if (!other.GetComponentInChildren<PlayerChargingUp>().EmissionState())
            {
                chargingRay.RayChargeMethod();
            }
            other.GetComponentInChildren<PlayerChargingUp>().insideCharger = true;
            StartCoroutine(StartGlow(leavesMaterial));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            glowSound.Stop();
            other.GetComponentInChildren<PlayerChargingUp>().insideCharger = false;
            StopAllCoroutines();
            foreach (MeshRenderer leafRenderer in leavesMaterial)
            {
                leafRenderer.material.DisableKeyword("_EMISSION");
            }
        }
    }

    
}
