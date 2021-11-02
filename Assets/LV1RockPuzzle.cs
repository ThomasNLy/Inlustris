using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV1RockPuzzle : Puzzle
{

    public GameObject rock;
    public TriggerLights lightSwitch;
    public SpawnAssets assetSpawnScript;
    public Teleportation rockTeleport;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(rock))
        {
            puzzleCompleted = true;
            Destroy(rock.GetComponent<Rigidbody>());
            GameObject indicatorRef = Instantiate(puzzleFinishedIndicator, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
            StartCoroutine(StartGlow(rock.GetComponent<MeshRenderer>()));
            indicatorRef.GetComponent<PuzzleFinishedIndicator>().target = thisStarVertex.starRef.transform;
            indicatorRef.GetComponent<PuzzleFinishedIndicator>().lightSwitch = this.lightSwitch;
            indicatorRef.GetComponent<PuzzleFinishedIndicator>().thisStarVertex = this.thisStarVertex;
            indicatorRef.GetComponent<PuzzleFinishedIndicator>().assetSpawnSwitch = assetSpawnScript;
            indicatorRef.GetComponent<PuzzleFinishedIndicator>().starCharge = thisStarVertex.starRef.GetComponent<StarPickedUp>().charge;
            indicatorRef.GetComponent<PuzzleFinishedIndicator>().fullyChargedSound = thisStarVertex.starRef.GetComponent<StarPickedUp>().fullyChargedSound;
            Destroy(rockTeleport.gameObject);
        }
    }

    private IEnumerator StartGlow(MeshRenderer meshRenderer)
    {
        Color startColor = meshRenderer.material.color;
        while (startColor.r < 1 || startColor.g < 1 || startColor.b < 1)
        {
            startColor.r += 1.0f / (255);
            startColor.g += 1.0f / (255);
            startColor.b += 1.0f / (255);
            meshRenderer.material.SetVector("_EmissionColor", startColor);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
