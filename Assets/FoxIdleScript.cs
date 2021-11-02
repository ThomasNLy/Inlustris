using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxIdleScript : MonoBehaviour
{

    public GameObject foxRef;
    public GameObject foxIdleRef;
    bool fading;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("StarsNotCollected").transform.childCount <= 0)
            Destroy(gameObject);

        Color temp = GetComponentInChildren<Renderer>().material.color;
        temp.a = 0;
        GetComponentInChildren<Renderer>().material.color = temp;
        StartCoroutine("HitRestoreColor", GetComponentInChildren<Renderer>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox") && !fading)
        {
            Instantiate(foxRef, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator HitRestoreColor(Renderer foxRenderer)
    {
        if (foxRenderer != null)
        {
            Color temp = foxRenderer.material.color;
            while (temp.a <= 1 && foxRenderer != null)
            {
                temp.a += 0.02f;
                foxRenderer.material.color = temp;
                yield return new WaitForSeconds(0.01f);
            }
        }
        StartCoroutine("HitFadeColor", foxRenderer);
    }

    IEnumerator HitFadeColor(Renderer foxRenderer)
    {
        yield return new WaitForSeconds(Random.Range(6.0f, 7.0f));
        fading = true;
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
                Transform playerTF = GameObject.FindGameObjectWithTag("Player").transform;
                Vector3 randomPosAroundPlayer = playerTF.position + playerTF.forward * 300.0f + playerTF.right * (Random.Range(-200f, 200f));
                GameObject idleRef = Instantiate(foxIdleRef, randomPosAroundPlayer, Quaternion.identity);
                idleRef.name = "foxIdleCopy";
                Destroy(foxRenderer.transform.parent.gameObject);
            }
        }
    }
}
