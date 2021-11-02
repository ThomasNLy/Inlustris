using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part14to19Lights : MonoBehaviour {
    public TriggerLights lightSwitch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("dolphinCopy"))
        {
            if (lightSwitch != null)
                lightSwitch.GraduallyTurnOnOffLightsMethod();
        }
    }
}
