using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCamera : MonoBehaviour {

    Camera[] cameras;
    

    // Use this for initialization
    void Start ()
    {
        cameras = GameObject.FindObjectsOfType<Camera>();
        
    }

    public void ActivateCutSceneCamera()
    {
        foreach (Camera c in cameras)
        {
            if (c.Equals(GetComponent<Camera>()))
                c.enabled = true;
            else
                c.enabled = false;
        }
    }

    public void DisableCutSceneCamera()
    {
        foreach (Camera c in cameras)
        {
            if (c.Equals(GetComponent<Camera>()))
                c.enabled = false;
            else
                c.enabled = true;
        }
    }
}
