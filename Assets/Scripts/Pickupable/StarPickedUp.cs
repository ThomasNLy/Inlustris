using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickedUp : MonoBehaviour
{

    public StarVertex thisStarVertex;
    StarCounter starCounter;
    StarsConnectedCounter starsConnectedCounter;
    Vector3 currentVel;
    public float smoothTime = 0.5f;
    public float smoothSpeed = 10000.0f;
    public bool counterTriggered;
    public bool movementTriggered;
    public bool lineMovementTriggered;
    bool allLinesConnected;
    public bool hasPuzzle;
    public ParticleSystem charge;
    public AudioSource fullyChargedSound;

    // Update is called once per frame
    void Start()
    {
        thisStarVertex.starRef = gameObject;
        starCounter = GameObject.FindGameObjectWithTag("StarCounter").GetComponent<StarCounter>();
        starsConnectedCounter = GameObject.FindGameObjectWithTag("StarCounter").GetComponent<StarsConnectedCounter>();
        charge.Stop();
        fullyChargedSound.Stop();
        if (!hasPuzzle)
        {
            charge.Play();
            fullyChargedSound.Play();
            starCounter.AddCount();
            thisStarVertex.IsInPlace = true;
            thisStarVertex.CheckAndDrawLines(thisStarVertex);
        }
    }

    private void Update()
    {
        if (movementTriggered)
        {

            if (Vector3.Distance(transform.position, thisStarVertex.transform.position) > 0)
            {
                if (Vector3.Distance(transform.position, thisStarVertex.transform.position) < 0.01f && !counterTriggered)
                {
                    starsConnectedCounter.AddCount();
                    counterTriggered = true;
                }
                transform.position = Vector3.SmoothDamp(transform.position, thisStarVertex.transform.position, ref currentVel, smoothTime, smoothSpeed);
            }
            else
            {
                movementTriggered = false;
            }
            
        }
        if (lineMovementTriggered)
        {
            foreach (EnhancedLineRenderer elr in thisStarVertex.enhancedLineRenderers)
            {
                allLinesConnected = allLinesConnected || Vector3.Distance(elr.end.starRef.transform.position, elr.end.transform.position) > 0;
                if (allLinesConnected)
                {
                    elr.thisLineRenderer.SetPosition(0, transform.position);
                    elr.thisLineRenderer.SetPosition(elr.thisLineRenderer.positionCount - 1, elr.end.starRef.transform.position);
                }
                else
                {
                    lineMovementTriggered = false;
                    break;
                }
            }
        }
    }
}
