using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererSpeedBoost : MonoBehaviour {

    public GameObject playerRef;
    public GameObject starRaySoundPrefab;
    public GameObject starRaySoundCopy;
    bool touchingLine;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (touchingLine)
        {
            GameObject.FindGameObjectWithTag("PlayerHitbox").GetComponent<Collider>().isTrigger = true;
            playerRef.GetComponent<Rigidbody>().velocity = playerRef.transform.forward * 100.0f;
        }
        else
        {
            GameObject.FindGameObjectWithTag("PlayerHitbox").GetComponent<Collider>().isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            starRaySoundCopy = Instantiate(starRaySoundPrefab, playerRef.transform.position, Quaternion.identity);
            touchingLine = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            starRaySoundCopy.transform.position = playerRef.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            touchingLine = false;
            Destroy(starRaySoundCopy);
        }
    }
}
