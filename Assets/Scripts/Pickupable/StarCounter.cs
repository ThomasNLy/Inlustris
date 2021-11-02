using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCounter : Counter {

    Transform powerUpGroup;
    float scaleAmount;

    private void Awake()
    {
        Amount = 0;
    }

    private void Start()
    {
        powerUpGroup = GameObject.FindGameObjectWithTag("StarsCollected").transform;
        scaleAmount = 0.4f;
    }

    public override void SubtractCount()
    {
        base.SubtractCount();
        if (powerUpGroup.childCount > 0)
            Destroy(powerUpGroup.GetChild(powerUpGroup.childCount - 1).gameObject);
    }

    public virtual GameObject CreatePickedUpRef(string prefabname, GameObject starObj)
    {
        StarVertex starVertex = starObj.GetComponent<StarPickup>().thisStarVertex;
        GameObject pickedUpStar = Instantiate((GameObject)Resources.Load("prefabs/" + prefabname + "pickedup", typeof(GameObject)), starObj.transform.position, Quaternion.identity);
        pickedUpStar.transform.parent = powerUpGroup;
        pickedUpStar.transform.localScale = pickedUpStar.transform.localScale * scaleAmount;
        return pickedUpStar;
    }
}
