using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLights : MonoBehaviour {

    public float targetIntensity;

    private IEnumerator GraduallyTurnOnLights()
    {
        while (true)
        {
            bool allLightsOn = true;
            foreach (Transform lightObj in transform)
            {
                if (lightObj.GetComponent<Light>().intensity <= targetIntensity)
                {
                    lightObj.GetComponent<Light>().intensity += 0.05f;
                    allLightsOn = allLightsOn && lightObj.GetComponent<Light>().intensity >= targetIntensity;
                }
            }
            if (allLightsOn)
                break;
            yield return new WaitForSeconds(0.001f);
        }
    }

    private IEnumerator GraduallyTurnOnOffLights()
    {
        while (true)
        {
            bool allLightsOn = true;
            foreach (Transform lightObj in transform)
            {
                if (lightObj.GetComponent<Light>().intensity <= targetIntensity)
                {
                    lightObj.GetComponent<Light>().intensity += 0.1f;
                    allLightsOn = allLightsOn && lightObj.GetComponent<Light>().intensity >= targetIntensity;
                }
            }
            if (allLightsOn)
                break;
            yield return new WaitForSeconds(0.001f);
        }

        while (true)
        {
            bool allLightsOff = true;
            foreach (Transform lightObj in transform)
            {
                if (lightObj.GetComponent<Light>().intensity >= 0)
                {
                    lightObj.GetComponent<Light>().intensity -= 0.1f;
                    allLightsOff = allLightsOff && lightObj.GetComponent<Light>().intensity <= 0;
                }
            }
            if (allLightsOff)
                break;
            yield return new WaitForSeconds(0.001f);
        }
    }

    public void GraduallyTurnOnLightsMethod()
    {
        StartCoroutine(GraduallyTurnOnLights());
    }

    public void GraduallyTurnOnOffLightsMethod()
    {
        StartCoroutine(GraduallyTurnOnOffLights());
    }
}
