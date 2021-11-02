using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour {

    public Transform teleportLocation;
    public bool cooldown;
    public ScreenWhiteToFade tempWhiteScreen;
    public float whiteScreenSlowFactor;
    public float teleportOffset;
    public AudioSource teleportSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox") && !cooldown)
        {
            StartCoroutine(SetCooldown(other));
        }
    }

    public IEnumerator SetCooldown(Collider other)
    {
        cooldown = true;
        teleportSound.Play();
        other.transform.parent.position = teleportLocation.position + teleportLocation.forward * teleportOffset;
        StartCoroutine(tempWhiteScreen.WhiteToFade(0.0f, Mathf.Clamp(whiteScreenSlowFactor, 1, Mathf.Infinity)));
        yield return new WaitForSeconds(1.0f);
        cooldown = false;
    }
}
