using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuWhaleMovement : MonoBehaviour {

    public Transform[] waypoint;
    public float speed;

    private int current;


    // Use this for initialization
	void Start () {
        speed = 5f;

	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position != waypoint[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, waypoint[current].position, speed * Time.deltaTime);
            this.transform.position = pos;
        }

        else
            current = (current + 1) % waypoint.Length;
    }
}
