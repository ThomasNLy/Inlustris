using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetWithRingGlow : Glow
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            other.GetComponentInChildren<PlayerChargingUp>().AddCharge();
            other.GetComponentInChildren<PlayerChargingUp>().insideCharger = true;
            StartCoroutine(StartGlow(GetComponent<MeshRenderer>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            other.GetComponentInChildren<PlayerChargingUp>().insideCharger = false;
            StopAllCoroutines();
            GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
