using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinMovement : MonoBehaviour
{
    IEnumerator revealFadeRef;
    public LineRenderer lineRendererToFollow;
    private int currentPoint;
    public GameObject dolphinRef;
    public Transform startPoint;
    public float speed;

    private void Start()
    {
        IEnumerator revealFadeRef = RevealFade(GetComponentInChildren<Renderer>());
        StartCoroutine(revealFadeRef);
    }

    private void Update()
    {
        TraversePoints();
    }

    public void TraversePoints()
    {
        if (currentPoint < (lineRendererToFollow.positionCount - 1))
        {
            
            if (Vector3.Distance(transform.position, lineRendererToFollow.GetPosition(currentPoint)) > 0.0f)
            {
                transform.LookAt(lineRendererToFollow.GetPosition(currentPoint));
                transform.position = Vector3.MoveTowards(transform.position, lineRendererToFollow.GetPosition(currentPoint), 500.0f * Time.deltaTime);
            }
            else
            {
                currentPoint++;
            }
        }
        else
        {
            transform.position += transform.forward * speed;
        }
    }

    IEnumerator RevealFade(Renderer dolphinRenderer)
    {
        Color temp = dolphinRenderer.material.color;
        temp.a = 0;
        dolphinRenderer.material.color = temp;
        while (temp.a < 1)
        {
            temp.a += (float)2 / 255;
            dolphinRenderer.material.color = temp;
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(1.0f);
        while (temp.a > 0)
        {
            temp.a -= (float)2 / 255;
            dolphinRenderer.material.color = temp;
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(0.5f);
        Instantiate(dolphinRef, startPoint.position, Quaternion.identity).name = "dolphinCopy";
        Destroy(gameObject);
    }
}
