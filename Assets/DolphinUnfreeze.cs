using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinUnfreeze : Unfreeze {

    public Transform startPoint;
    public LineRenderer lineRendererToFollow;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            GameObject dolphinRef = Instantiate(unfrozen, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
            dolphinRef.GetComponent<DolphinMovement>().lineRendererToFollow = this.lineRendererToFollow;
            dolphinRef.GetComponent<DolphinMovement>().startPoint = this.startPoint;
            dolphinRef.GetComponent<DolphinMovement>().speed = 10.0f;
            Destroy(gameObject);
        }
    }
}
