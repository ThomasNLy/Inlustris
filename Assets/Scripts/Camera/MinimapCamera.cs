using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour {

    Transform player;
    float startingCameraHeight;
    float shadowValue;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("PlayerHitbox").transform;
        startingCameraHeight = 900f;
        transform.position = new Vector3(player.position.x, startingCameraHeight + player.position.y, player.position.z);

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 posRef = transform.position;
        posRef.x = player.position.x;
        posRef.z = player.position.z;
        transform.position = posRef;
	}

    private void OnPreRender()
    {
        shadowValue = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }

    private void OnPostRender()
    {
        QualitySettings.shadowDistance = shadowValue;
    }
}
