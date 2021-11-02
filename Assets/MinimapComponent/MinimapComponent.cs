using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapComponent : MonoBehaviour {

    public Transform target;
    float startHeight;

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    // Use this for initialization
    void Start()
    {
        startHeight = 880f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, startHeight, target.position.z);
        transform.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
    }
}
