using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinPart14to19Unfreeze : DolphinUnfreeze
{
    public Part14to19Lights[] dolphinLightsTriggers;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            GameObject dolphinRef = Instantiate(unfrozen, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
            dolphinRef.GetComponent<DolphinMovement>().lineRendererToFollow = this.lineRendererToFollow;
            dolphinRef.GetComponent<DolphinMovement>().startPoint = this.startPoint;
            dolphinRef.GetComponent<DolphinMovement>().speed = 1.0f;
            dolphinRef.name = "dolphinCopy";
            Destroy(gameObject);
        }
    }
}
