using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFinishedIndicator : MonoBehaviour {

    public Transform target;
    public ParticleSystem.EmissionModule eModule;
    public ParticleSystem.MainModule mainModule;
    public Vector3 currentVelocity;
    public TriggerLights lightSwitch;
    public SpawnAssets assetSpawnSwitch;
    public StarVertex thisStarVertex;
    public ParticleSystem starCharge;
    public AudioSource fullyChargedSound;
    public bool selfDestructActivated;

    public AudioSource indicatorChargingSound;
    public AudioSource indicatorFiringSound;
    public AudioSource indicatorTravelSound;
    public AudioSource indicatorHitSound;

    bool activated;
	// Use this for initialization
	void Start () {
        mainModule = GetComponent<ParticleSystem>().main;
        mainModule.simulationSpace = ParticleSystemSimulationSpace.Local;
        eModule = GetComponent<ParticleSystem>().emission;
        StartCoroutine("DelayTravel");
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, target.position) > 0.0f)
        {
            if (activated)
            {
                if (Vector3.Distance(transform.position, target.position) > 1.0f)
                    transform.position = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, 2.5f);
                else
                    transform.position = Vector3.MoveTowards(transform.position, target.position, 10.0f);
            }
            else
                transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + GameObject.FindGameObjectWithTag("Player").transform.forward * 50.0f 
                    + GameObject.FindGameObjectWithTag("Player").transform.up * 10.0f;
        }
        else
        {
            if (!selfDestructActivated)
            {
                selfDestructActivated = true;
                eModule.enabled = false;
                StartCoroutine("DestroyAfterTime");
            }
        }
	}

    IEnumerator DelayTravel()
    {
        StartCoroutine(Camera.main.GetComponent<ThirdPersonCamera>().LookAtTarget(transform));
        yield return new WaitForSeconds(3.0f);
        indicatorChargingSound.Stop();
        indicatorFiringSound.Play();
        indicatorTravelSound.Play();
        activated = true;
        mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
    }

    IEnumerator DestroyAfterTime()
    {
        StartCoroutine(Camera.main.GetComponent<ThirdPersonCamera>().LookAtTargetEnd());
        indicatorTravelSound.Stop();
        indicatorHitSound.Play();
        yield return new WaitForSeconds(1.0f);
        thisStarVertex.IsInPlace = true;
        thisStarVertex.CheckAndDrawLines(thisStarVertex);
        GameObject.FindGameObjectWithTag("StarCounter").GetComponent<StarCounter>().AddCount();
        fullyChargedSound.Play();
        starCharge.Play();
        if (lightSwitch != null)
            lightSwitch.GraduallyTurnOnLightsMethod();
        if (assetSpawnSwitch != null)
            assetSpawnSwitch.SpawnObjects();
        Destroy(gameObject);
    }
}
