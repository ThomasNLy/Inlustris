using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupFade : MonoBehaviour {

    public Material opaqueMat;
    public Material transparentMat;

    private void Start()
    {
        foreach (Transform t in transform)
        {
            Color c = transparentMat.color;
            c.a = 0;
            transparentMat.color = c;
            t.GetComponent<MeshRenderer>().material = transparentMat;
            t.gameObject.SetActive(false);
        }
    }

    public IEnumerator RevealAll()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);
        }
        while (true)
        {
            int revealedCount = 0;
            foreach (Transform t in transform)
            {
                Material m = t.GetComponent<MeshRenderer>().material;
                Color c = m.color;
                c.a += 0.1f;
                m.color = c;
                if (c.a >= 1)
                    revealedCount++;
            }
            if (revealedCount == transform.childCount)
                break;
            yield return new WaitForSeconds(0.01f);
        }
        foreach (Transform t in transform)
        {
            t.GetComponent<MeshRenderer>().material = opaqueMat;
        }
    }
}
