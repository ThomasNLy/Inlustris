using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargingUp : MonoBehaviour {

    public ParticleSystem[] pSystems;
    public ParticleSystem[] tailSystems;
    public bool insideCharger;

	// Use this for initialization
	void Start () {
        AddCharge();
    }

    public void AddCharge()
    {
        GetComponentInParent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        for (int i = 0; i < pSystems.Length; i++)
        {
            ParticleSystem.EmissionModule eModule = pSystems[i].emission;
            eModule.enabled = true;
        }
        for (int i = 0; i < tailSystems.Length; i++)
        {
            tailSystems[i].Play();
        }
    }

    public void RemoveCharge()
    {
        GetComponentInParent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        for (int i = 0; i < pSystems.Length; i++)
        {
            ParticleSystem.EmissionModule eModule = pSystems[i].emission;
            eModule.enabled = false;
        }
        for (int i = 0; i < tailSystems.Length; i++)
        {
            tailSystems[i].Stop();
        }
    }

    public bool EmissionState()
    {
        return pSystems[0].emission.enabled;
    }
}
