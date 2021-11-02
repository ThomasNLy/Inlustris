using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAssets : MonoBehaviour
{
    /*
     * When ever the player interacts/ picks up the star(s) scattered throughout the map a random asset will 
     * spawn in at a prespecified position near that star 
     * 
     */


    //public GameObject assetToSpawnIn1;
    //public GameObject assetToSpawnIn2;
    public GameObject[] assetToSpawnIn; //List containing the objects that can possible spawn in at the specified location
    public GameObject[] spawnPoints; //reference to the spawn point object where the item will spawn in at

    private int numberOfVariousAssets; //# of various assets that are able to spawn in 
    public float randomScaleMin;
    public float randomScaleMax;

    GameObject assetSpawningIn;

    // Use this for initialization
    void Start()
    {
        numberOfVariousAssets = assetToSpawnIn.Length;
        
    }

    /*
     *When the player star's collider detects that it is colliding with the player/ is within the range to be picked up
     * a "random" asset will spawn in 
     * 
     */
    public void SpawnObjects()
    {
        if (spawnPoints.Length > 0)
        {
            foreach (GameObject sp in spawnPoints)
            {
                int randomAsset = Random.Range(0, numberOfVariousAssets);
                assetSpawningIn = assetToSpawnIn[randomAsset];
                GameObject assetRef = Instantiate(assetSpawningIn, sp.transform.position, Quaternion.Euler(sp.transform.localEulerAngles.x, sp.transform.localEulerAngles.y, sp.transform.localEulerAngles.z));
                float randomScale = Random.Range(randomScaleMin, randomScaleMax);
                assetRef.transform.localScale *= randomScale;
                if (assetRef.GetComponent<Animator>() != null)
                {
                    assetRef.GetComponent<Animator>().speed = Random.Range(0.5f, 1.2f);
                }

            }
        }
    }


}
