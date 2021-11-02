using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPickup : MonoBehaviour {

    public StarVertex thisStarVertex;
    StarCounter p;
    public GameObject[] starCountIndicators;
    public GameObject indicator;
    public bool isTargeted;
    public GameObject puzzleGameObj;
    public GameObject lightsAroundStar;
    public SpawnAssets spawnRef;
    public TreePlayerRay rayFromTree;

    private void Start()
    {
        starCountIndicators = GameObject.FindGameObjectsWithTag("StarCountIndicator");
        indicator = Instantiate((GameObject)Resources.Load("prefabs/" + "starIcon", typeof(GameObject)));
        indicator.AddComponent<MinimapComponent>();
        indicator.GetComponent<MinimapComponent>().Target = transform;
        p = GameObject.FindGameObjectWithTag("StarCounter").GetComponent<StarCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox") && other.GetComponentInChildren<PlayerChargingUp>().EmissionState())
        {
            other.GetComponentInChildren<PlayerChargingUp>().RemoveCharge();
            other.GetComponentInParent<MovementGeneratedParticles>().SoundFadeOutMethod();
            if (other.GetComponentInChildren<PlayerChargingUp>().insideCharger)
            {
                rayFromTree.RayChargeMethod();
            }
                
            GameObject starPickedUpRef = p.CreatePickedUpRef(transform.GetChild(0).name, gameObject);
            starPickedUpRef.GetComponent<StarPickedUp>().thisStarVertex = thisStarVertex;
            
            foreach (GameObject starIndicator in starCountIndicators)
            {
                starIndicator.GetComponent<StarCountIndicator>().ActivateStarIndicators();
            }
            if (puzzleGameObj != null)
            {
                starPickedUpRef.GetComponent<StarPickedUp>().hasPuzzle = true;
                puzzleGameObj.SetActive(true);
            }
            if (lightsAroundStar != null)
            {
                lightsAroundStar.GetComponent<TriggerLights>().GraduallyTurnOnLightsMethod();
            }
            if (spawnRef != null)
            {
                spawnRef.SpawnObjects();
            }
            Destroy(indicator);
            Destroy(transform.parent.gameObject);
        }
    }
}
