using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickupable : MonoBehaviour
{

    bool inRange;
    Sprite s;
    Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            SlotManager slotmanager = GameObject.FindGameObjectWithTag("UI").GetComponent<InventoryCaller>().GetInventoryPanel().GetComponentInChildren<SlotManager>();
            if (!slotmanager.IsFull())
            {
                Transform firstavailableslot = slotmanager.FirstEmptySlot().transform;
                GameObject g = new GameObject();
                g.AddComponent<Image>();
                g.GetComponent<Image>().sprite = s;
                g.AddComponent<InventoryDragger>();
                g.AddComponent<CanvasGroup>();
                g.AddComponent<Droppable>();
                g.AddComponent<ItemReference>();

                g.transform.SetParent(firstavailableslot.transform);
                g.transform.position = firstavailableslot.position;

                string prefabname = s.name + "pickedup";
                Debug.Log(prefabname);
                GameObject obj = Instantiate((GameObject)Resources.Load("prefabs/" + prefabname, typeof(GameObject)), playerPos.position, Quaternion.identity);
                g.GetComponent<ItemReference>().itemRef = obj;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other) //don't use OnTriggerStay for button statements, use in Update instead
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            s = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
