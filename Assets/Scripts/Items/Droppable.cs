using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Droppable : MonoBehaviour {

    public void Drop() {
        Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        string s = GetComponent<Image>().sprite.name;
        GameObject obj = (GameObject)Resources.Load("prefabs/" + s, typeof(GameObject));
        Instantiate(obj, playerPos.position, Quaternion.identity);
        Destroy(GetComponent<ItemReference>().itemRef);
        Destroy(gameObject);
    }
}
