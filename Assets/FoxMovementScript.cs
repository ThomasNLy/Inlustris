using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMovementScript : MonoBehaviour {

    public GameObject foxIdleRef;
    public Transform target;
    Transform starsNotCollectedGroup;

    // Use this for initialization
    void Start () {
        starsNotCollectedGroup = GameObject.FindGameObjectWithTag("StarsNotCollected").transform;
        ResetTarget();
        StartCoroutine("SpawnFoxMoving");
    }

    public void ResetTarget()
    {
        if (starsNotCollectedGroup.childCount > 0)
            target = starsNotCollectedGroup.GetChild(0);
    }

    IEnumerator SpawnFoxMoving()
    {
        while (true)
        {
            float foxDelay = Random.Range(1.5f, 3.0f);

            IEnumerator moveFox = MoveFox(gameObject);
            IEnumerator colorChangeFade = HitFadeColor(GetComponentInChildren<Renderer>());

            StartCoroutine(moveFox);
            yield return new WaitForSeconds(foxDelay);
            yield return StartCoroutine(colorChangeFade);
        }
    }
    IEnumerator MoveFox(GameObject fox)
    {
        if (target != null)
        {
            fox.transform.LookAt(target, fox.transform.up);
        }
        else
        {
            ResetTarget();
        }

        while (fox != null)
        {
            fox.transform.position += fox.transform.forward * 3.0f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator HitFadeColor(Renderer foxRenderer)
    {
        if (foxRenderer != null)
        {
            Color temp = foxRenderer.material.color;
            while (temp.a >= 0 && foxRenderer != null)
            {
                temp.a -= 0.02f;
                foxRenderer.material.color = temp;
                yield return new WaitForSeconds(0.01f);
            }
            if (foxRenderer != null)
            {
                yield return new WaitForSeconds(Random.Range(5.0f, 7.0f));
                Transform playerTF = GameObject.FindGameObjectWithTag("Player").transform;
                Vector3 randomPosAroundPlayer = playerTF.position + playerTF.forward * 300.0f + playerTF.right * (Random.Range(-200f, 200f));
                Instantiate(foxIdleRef, randomPosAroundPlayer, Quaternion.identity);
                Destroy(foxRenderer.transform.parent.gameObject);
            }
        }
    }
}
