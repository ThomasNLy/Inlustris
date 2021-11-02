using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAuraTrigger : MonoBehaviour
{
    [SerializeField]
    ParticleSystem psOngoingAura;

    [SerializeField]
    ParticleSystem psGlow;

    ParticleSystem.EmissionModule psOngoingEM;
    ParticleSystem.EmissionModule psGlowEM;

    private IEnumerator myCoroutine;

    void Start()
    {
        psOngoingAura.Stop();
        psGlow.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            psOngoingAura.Play();
            psGlow.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            psOngoingAura.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            psGlow.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

}
