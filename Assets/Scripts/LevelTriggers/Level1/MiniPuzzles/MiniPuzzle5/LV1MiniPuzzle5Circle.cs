using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV1MiniPuzzle5Circle : MonoBehaviour {

    public ParticleSystemRenderer thisRenderer;
    public bool selected;
    public bool traversedThrough;
    public AudioSource selectedCircleSound;

    // Use this for initialization
    private void Awake()
    {
        thisRenderer = GetComponent<ParticleSystemRenderer>();
    }

    private void Start()
    {
        StartCoroutine(RevealFade(thisRenderer.material));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox") && !traversedThrough)
        {
            selectedCircleSound.Play();
            traversedThrough = true;
            Color colorRef = thisRenderer.material.color;
            colorRef.r = 0;
            colorRef.g = 0;
            thisRenderer.material.color = colorRef;
            GetComponentInParent<LV1MiniPuzzle5>().amountOfCirclesPassed++;
            if (selected)
                GetComponentInParent<LV1MiniPuzzle5>().amountOfCirclesCorrectlyPassed++;
            GetComponentInParent<LV1MiniPuzzle5>().ResetTimer();
        }
    }

    IEnumerator RevealFade(Material circleMaterial)
    {
        Color temp = circleMaterial.color;
        temp.a = 0;
        circleMaterial.color = temp;
        while (temp.a < 1)
        {
            temp.a += (float)1 / 255;
            circleMaterial.color = temp;
            yield return new WaitForSeconds(0.001f);
        }
    }
}
