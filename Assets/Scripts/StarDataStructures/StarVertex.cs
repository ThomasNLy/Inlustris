using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedLineRenderer
{
    public LineRenderer thisLineRenderer;
    public StarVertex start;
    public StarVertex end;

    public EnhancedLineRenderer(LineRenderer thisLineRenderer, StarVertex start, StarVertex end)
    {
        this.thisLineRenderer = thisLineRenderer;
        this.start = start;
        this.end = end;
    }
}

public class StarVertex : MonoBehaviour
{
    public GameObject starRef;
    public Material starRayMaterial;
    public GameObject starRaySoundObj;
    
    private bool isInPlace = false;
    public bool IsInPlace
    {
        get { return isInPlace; }
        set { isInPlace = value; }
    }
    public List<EnhancedLineRenderer> enhancedLineRenderers = new List<EnhancedLineRenderer>();
    public List<StarVertex> starsConnectedTo = new List<StarVertex>();
    StarsConnectedCounter starsConnectedCounter;


    private void Start()
    {
        starsConnectedCounter = GameObject.FindGameObjectWithTag("StarCounter").GetComponent<StarsConnectedCounter>();
    }

    public void CheckAndDrawLines(StarVertex thisSV)
    {
        foreach (StarVertex sv in starsConnectedTo)
        {
            if (sv.IsInPlace && thisSV.isInPlace)
            {
                DrawLine(thisSV, sv);
            }

        }
    }

    private void DrawLine(StarVertex start, StarVertex end)
    {
        GameObject starLine = new GameObject();
        starLine.name = "GeneratedLine";
        starLine.transform.parent = transform;

        GameObject starLineCollider = new GameObject();
        starLineCollider.AddComponent<BoxCollider>().size = new Vector3(10.0f, 10.0f, 10.0f);
        starLineCollider.GetComponent<BoxCollider>().isTrigger = true;
        starLineCollider.AddComponent<LineRendererSpeedBoost>();
        starLineCollider.GetComponent<LineRendererSpeedBoost>().starRaySoundPrefab = starRaySoundObj;
        starLineCollider.transform.parent = starLine.transform;
        starLine.AddComponent<LineRenderer>();
        starLine.GetComponent<LineRenderer>().material = starRayMaterial;
        LineRenderer lineRenderer = starLine.GetComponent<LineRenderer>();
        EnhancedLineRenderer enhancedLineRenderer = new EnhancedLineRenderer(lineRenderer, start, end);
        
        enhancedLineRenderers.Add(enhancedLineRenderer);
        IEnumerator starTravel = LineToTarget(enhancedLineRenderer.thisLineRenderer, enhancedLineRenderer.start.starRef.transform.position, enhancedLineRenderer.end.starRef.transform.position, starLineCollider.GetComponent<BoxCollider>());
        StartCoroutine(starTravel);
    }

    IEnumerator LineToTarget(LineRenderer lineRenderer, Vector3 start, Vector3 end, BoxCollider boxCollider)
    {
        Vector3 currentVel = Vector3.zero;
        float smoothTime = 0.5f;
        float smoothSpeed = 1000.0f;
        //lineRenderer.material = new Material(Shader.Find("Diffuse"));
        lineRenderer.startColor = Color.white;
        lineRenderer.startWidth = 10.0f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, start);
        boxCollider.transform.position = lineRenderer.GetPosition(0);
        boxCollider.transform.LookAt(end);

        while (Vector3.Distance(lineRenderer.GetPosition(1), end) > 0.1f)
        {
            Vector3 currentEndPos = lineRenderer.GetPosition(1);
            currentEndPos = Vector3.SmoothDamp(currentEndPos, end, ref currentVel, smoothTime, smoothSpeed);
            lineRenderer.SetPosition(1, currentEndPos);
            boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, Vector3.Distance(start, currentEndPos));
            boxCollider.center = new Vector3(0, 0, Vector3.Distance(start, currentEndPos)/2);
            yield return new WaitForSeconds(0.00001f);
        }
    }
}
