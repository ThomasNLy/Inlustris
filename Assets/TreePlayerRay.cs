using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlayerRay : MonoBehaviour
{

    private Vector3 playerPos;
    //private Vector3 currentVelocity;
    public GameObject rayToTarget;
    GameObject rayToTargetRef;
    GameObject playerObj;
    public PlayerChargingUp chargeScript;
    bool firstTime = true;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObj.transform.position;
    }

    public void RayChargeMethod()
    {
        if (rayToTargetRef == null)
            StartCoroutine(RayCharge());
    }

    private void FixedUpdate()
    {
        playerPos = playerObj.transform.position;
    }

    private IEnumerator RayCharge()
    {
        rayToTargetRef = Instantiate(rayToTarget, transform.position, Quaternion.identity);
        if (firstTime)
        {
            StartCoroutine(Camera.main.GetComponent<ThirdPersonCamera>().LookAtTarget(rayToTargetRef.transform));
            firstTime = false;
        }
        while (Vector3.Distance(rayToTargetRef.transform.position, playerPos) > 0.0f)
        {
            
            //rayToTargetRef.transform.position = Vector3.SmoothDamp(rayToTargetRef.transform.position, playerPos, ref currentVelocity, 0.8f);
            rayToTargetRef.transform.position = Vector3.MoveTowards(rayToTargetRef.transform.position, playerPos, 100.0f * Time.deltaTime);
            yield return new WaitForSeconds(0.001f);
        }
        ParticleSystem[] rayParticleSystems = rayToTargetRef.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in rayParticleSystems)
        {
            ParticleSystem.EmissionModule rayEM = ps.emission;
            rayEM.enabled = false;
        }
        chargeScript.AddCharge();
        if (playerObj.GetComponent<MovementGeneratedParticles>().MovementKeyPressed())
            playerObj.GetComponent<MovementGeneratedParticles>().tailParticleSound.Play();
        StartCoroutine("DestroyAfterTime", rayToTargetRef);
    }

    private IEnumerator DestroyAfterTime(GameObject objToDestroy)
    {
        StartCoroutine(Camera.main.GetComponent<ThirdPersonCamera>().LookAtTargetEnd());
        foreach (AudioSource a in objToDestroy.GetComponents<AudioSource>())
        {
            if (a.clip.name.Equals("treeEnergyBlastTravel"))
                a.Stop();
            if (a.clip.name.Equals("treeEnergyBlastHit"))
                a.Play();
        }
        yield return new WaitForSeconds(2.0f);
        Destroy(objToDestroy);
    }
}
