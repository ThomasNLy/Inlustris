using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour {
    
    PowerupCounter p;
    GameObject indicator;

    private void Start()
    {
        indicator = Instantiate((GameObject)Resources.Load("prefabs/" + "starIcon", typeof(GameObject)));
        indicator.AddComponent<MinimapComponent>();
        indicator.GetComponent<MinimapComponent>().Target = transform;
        p = GameObject.FindGameObjectWithTag("PowerupCounter").GetComponent<PowerupCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerPickup")) {
            p.CreatePickedUpRef(transform.GetChild(0).name);
            Destroy(indicator);
            Destroy(gameObject);
        }
    }
}
