using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockToRockScript : MonoBehaviour {

    public GameObject targetArea;
    public GameObject rockRay;
    GameObject rockRayRef;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<RockToRockScript>() != null)
        {
            StartCoroutine(RockRayIndicator());
        }
    }

    private IEnumerator RockRayIndicator()
    {
        rockRayRef = Instantiate(rockRay, transform.position, Quaternion.identity);
        ParticleSystem.MainModule mainModule = rockRayRef.GetComponent<ParticleSystem>().main;
        mainModule.startColor = GetComponentInParent<MeshRenderer>().material.color;
        while (Vector3.Distance(rockRayRef.transform.position, targetArea.transform.position) > 0.0f)
        {

            //rayToTargetRef.transform.position = Vector3.SmoothDamp(rayToTargetRef.transform.position, playerPos, ref currentVelocity, 0.8f);
            rockRayRef.transform.position = Vector3.MoveTowards(rockRayRef.transform.position, targetArea.transform.position, 100.0f * Time.deltaTime);
            yield return null;
        }
        StartCoroutine("DestroyAfterTime", rockRayRef);
    }

    private IEnumerator DestroyAfterTime(GameObject objToDestroy)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(objToDestroy);
    }
}
