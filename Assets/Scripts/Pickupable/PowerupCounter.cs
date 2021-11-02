using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCounter : Counter {

    Transform playerPos;
    Transform powerUpGroup;
    float scaleAmount;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("PlayerPickup").transform;
        powerUpGroup = GameObject.FindGameObjectWithTag("PowerupGroup").transform;
        scaleAmount = 0.4f;
    }

    public override void SubtractCount()
    {
        base.SubtractCount();
        Destroy(powerUpGroup.GetChild(Amount).gameObject);
    }

    public virtual void CreatePickedUpRef(string prefabname)
    {
        AddCount();
        GameObject powerupref = Instantiate((GameObject)Resources.Load("prefabs/" + prefabname + "pickedup", typeof(GameObject)), playerPos.position, Quaternion.identity);
        powerupref.transform.parent = powerUpGroup;
        powerupref.transform.localScale = powerupref.transform.localScale * scaleAmount;
        if (Amount <= 3)
        {
            powerupref.AddComponent<CubeRotation>();
            powerupref.GetComponent<CubeRotation>().target = playerPos;
        }
        else
        {
            powerupref.AddComponent<CubeTrailing>();
        }
    }
}
