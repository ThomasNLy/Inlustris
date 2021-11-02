using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBezier : MonoBehaviour {

    public LineRenderer lineRenderer;
    public Transform point0, point1, point2, point3;

    public int numPoints;
    private Vector3[] positions;

    private void Start()
    {
        positions = new Vector3[numPoints];
        lineRenderer.positionCount = numPoints;
        DrawCubicCurve();
    }

    private void Update()
    {
        DrawCubicCurve();
    }

    private void DrawCubicCurve()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            positions[i] = CalculateCubicBezier(t, point0.position, point1.position, point2.position, point3.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateCubicBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Mathf.Pow((1 - t), 3) * p0 + 3 * Mathf.Pow((1 - t), 2) * t * p1 + 3 * (1 - t) * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
    }
}
