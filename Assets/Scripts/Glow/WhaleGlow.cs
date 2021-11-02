using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleGlow : Glow
{
    MeshRenderer whaleMesh;
	// Use this for initialization
	void Start () {
        whaleMesh = this.gameObject.GetComponent<MeshRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
        StartGlow(whaleMesh);
	}
}
