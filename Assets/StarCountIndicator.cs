using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCountIndicator : MonoBehaviour
{
    public int amount;
    public GameObject starIndicator;

    private void Start()
    {
        CreateStarIndicators();
    }

    private void CreateStarIndicators()
    {
        GameObject[] starIndicators = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            transform.eulerAngles = new Vector3(0.0f, (360.0f / amount) * (i + 1), 0.0f);
            Debug.Log(transform.eulerAngles.y);
            GameObject starIndicatorCopy = Instantiate(starIndicator, transform.position + transform.forward * 20.0f, Quaternion.Euler(-90, 0, 0));
            starIndicators[i] = starIndicatorCopy;
        }

        foreach (GameObject g in starIndicators)
        {
            g.transform.parent = transform;
        }
    }

    public void ActivateStarIndicators()
    {
        for (int i = 0; i < amount; i++)
        {
            foreach (ParticleSystem ps in transform.GetChild(i).GetComponentsInChildren<ParticleSystem>())
            {
                ps.Play();
            }
        }
    }
}
