using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unfreeze : MonoBehaviour {

    public GameObject unfrozen;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            Instantiate(unfrozen, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
            Destroy(gameObject);
        }
    }
}
