using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRotateSpawn : MonoBehaviour
{
    public float xLocalRotate;
    public float yLocalRotate;
    public float zLocalRotate;

    private void Start()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + xLocalRotate, transform.localEulerAngles.y + yLocalRotate, transform.localEulerAngles.z + zLocalRotate);
    }
}
