using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BezierCurve : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public Transform[] oneMinusTPoints;
    public Transform[] tPoints;
    public int nop;
    public int noCP;
    public Vector3[] positions;
    public GameObject[] controlPointRefs;


    private void Start()
    {
        //controlPointRefs = new GameObject[noCP];
        //BezierAddControlPoints(noCP);
        lineRenderer.positionCount = nop;
        positions = new Vector3[nop];

    }

    void Update()
    {
        DrawCurve();
    }

    public void DrawCurve()
    {
        for (int i = 0; i < nop; i++)
        {
            float t = i / (float)(nop - 1);
            positions[i] = BezierRecursion(t, 2, noCP, controlPointRefs[0].transform.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private void BezierAddControlPoints(int noCP) //working
    {
        oneMinusTPoints = new Transform[noCP - 1];
        tPoints = new Transform[noCP - 1];

        Debug.Log(noCP);
        Debug.Log(oneMinusTPoints.Length);
        Debug.Log(tPoints.Length);

        for (int i = 0; i < noCP; i++)
        {
            controlPointRefs[i] = new GameObject
            {
                name = "p" + i
            };

            if (i != noCP - 1)
                oneMinusTPoints[i] = controlPointRefs[i].transform;
            if (i != 0)
                tPoints[i - 1] = controlPointRefs[i].transform;
        }
    }

    public Vector3 BezierRecursion(float t, int count, int noCP, Vector3 previous)
    {
        if (count > noCP)
            return previous;
        if (noCP == 2)
            return GetLinearBezier(t, oneMinusTPoints[count - 2].position, tPoints[count - 2].position);
        return (1 - t) * previous + t * BezierRecursion(t, count + 1, noCP, GetLinearBezier(t, oneMinusTPoints[count - 2].position, tPoints[count - 2].position));
    }

    private Vector3 GetLinearBezier(float t, Vector3 p0, Vector3 p1)
    {
        return (1 - t) * p0 + t * p1;
    }
}