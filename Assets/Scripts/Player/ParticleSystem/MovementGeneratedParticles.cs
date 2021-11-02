using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGeneratedParticles : MonoBehaviour
{
    [SerializeField]
    ParticleSystem tailParticles;
    ParticleSystem.EmissionModule tailMoveEmission;
    [SerializeField]
    ParticleSystem tailBurstParticles;
    ParticleSystem.EmissionModule tailBurstEmission;

    [SerializeField]
    ParticleSystem charge;

    public AudioSource tailParticleSound;

    [SerializeField]
    GameObject tailBurstParticleSoundObj;

    Rigidbody playerRB;
    bool shiftPressed;
    bool movementHeld;
    float fadeTime = 2.0f;

    public IEnumerator fadeSound;

    // Use this for initialization
    void Start()
    {
        tailMoveEmission = tailParticles.emission;
        tailBurstEmission = tailBurstParticles.emission;
        tailMoveEmission.enabled = false;
        tailBurstEmission.enabled = false;
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ParticlesWhenMoving();
    }

    void ParticlesWhenMoving()
    {
        if (playerRB.velocity == Vector3.zero)
        {
            tailMoveEmission.enabled = false;
        }
        else
        {
            if (charge.emission.enabled)
            {
                tailMoveEmission.enabled = true;

                if (MovementKeyPressed() && !movementHeld)
                {
                    if (fadeSound != null)
                    {
                        StopCoroutine(fadeSound);
                        tailParticleSound.volume = 0.5f;
                    }
                    movementHeld = true;
                    tailParticleSound.Play();
                }
                else if (MovementKeyReleased())
                {
                    if (fadeSound != null)
                    {
                        StopCoroutine(fadeSound);
                    }
                    fadeSound = SoundFadeOut();
                    StartCoroutine(fadeSound);
                    movementHeld = false;
                }

                if (Input.GetKey(KeyCode.LeftShift) && !shiftPressed)
                {
                    shiftPressed = true;
                    tailBurstEmission.enabled = true;
                    tailBurstParticles.Play();
                    Instantiate(tailBurstParticleSoundObj, playerRB.transform.position, Quaternion.identity);
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    shiftPressed = false;
                    tailBurstParticles.Stop();
                    tailBurstEmission.enabled = false;
                }
            }
        }
    }

    public bool MovementKeyPressed()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }

    public bool MovementKeyReleased()
    {
        return (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)) && !MovementKeyPressed();
    }

    IEnumerator SoundFadeOut()
    {
        float startVolume = tailParticleSound.volume;

        while (tailParticleSound.volume > 0)
        {
            tailParticleSound.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        tailParticleSound.Stop();
        tailParticleSound.volume = startVolume;
    }

    public void SoundFadeOutMethod()
    {
        fadeSound = SoundFadeOut();
        StartCoroutine(fadeSound);
    }
}
