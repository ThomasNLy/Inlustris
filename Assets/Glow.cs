using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float rBound;

    [Range(0.0f, 1.0f)]
    public float gBound;

    [Range(0.0f, 1.0f)]
    public float bBound;

    public float slowSmoothValue;

    protected IEnumerator StartGlow(MeshRenderer meshRenderer)
    {
        Color startColor = Color.white;
        startColor.r = 0;
        startColor.g = 0;
        startColor.b = 0;

        meshRenderer.material.EnableKeyword("_EMISSION");

        while (startColor.r < rBound || startColor.g < gBound || startColor.b < bBound)
        {
            if (startColor.r < rBound)
                startColor.r += 1.0f / (255 * slowSmoothValue);
            if (startColor.g < gBound)
                startColor.g += 1.0f / (255 * slowSmoothValue);
            if (startColor.b < bBound)
                startColor.b += 1.0f / (255 * slowSmoothValue);
            meshRenderer.material.SetVector("_EmissionColor", startColor);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(FadeGlow(meshRenderer, startColor));
    }

    protected IEnumerator FadeGlow(MeshRenderer meshRenderer, Color startColor)
    {
        while (startColor.r > 0 || startColor.g > 0 || startColor.b > 0)
        {
            if (startColor.r > 0)
                startColor.r -= 1.0f / (255 * slowSmoothValue);
            if (startColor.g > 0)
                startColor.g -= 1.0f / (255 * slowSmoothValue);
            if (startColor.b > 0)
                startColor.b -= 1.0f / (255 * slowSmoothValue);
            meshRenderer.material.SetVector("_EmissionColor", startColor);

            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(StartGlow(meshRenderer));
    }

    protected IEnumerator StartGlow(MeshRenderer[] meshRenderers)
    {
        Color startColor = Color.white;
        startColor.r = 0;
        startColor.g = 0;
        startColor.b = 0;
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material.EnableKeyword("_EMISSION");
        }
        while (startColor.r < rBound || startColor.g < gBound || startColor.b < bBound)
        {
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                if (startColor.r < rBound)
                    startColor.r += 1.0f / (255 * slowSmoothValue);
                if (startColor.g < gBound)
                    startColor.g += 1.0f / (255 * slowSmoothValue);
                if (startColor.b < bBound)
                    startColor.b += 1.0f / (255 * slowSmoothValue);
                meshRenderer.material.SetVector("_EmissionColor", startColor);
            }
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(FadeGlow(meshRenderers, startColor));
    }

    protected IEnumerator FadeGlow(MeshRenderer[] meshRenderers, Color startColor)
    {
        while (startColor.r > 0 || startColor.g > 0 || startColor.b > 0)
        {
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                if (startColor.r > 0)
                    startColor.r -= 1.0f / (255 * slowSmoothValue);
                if (startColor.g > 0)
                    startColor.g -= 1.0f / (255 * slowSmoothValue);
                if (startColor.b > 0)
                    startColor.b -= 1.0f / (255 * slowSmoothValue);
                meshRenderer.material.SetVector("_EmissionColor", startColor);
            }
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(StartGlow(meshRenderers));
    }
}
