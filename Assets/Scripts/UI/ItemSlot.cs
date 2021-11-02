using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler {

    private int slotCount;

    public int SlotCount {
        get { return slotCount; }
        set { slotCount = value; }
    }

    public GameObject Item { //get item in this slot
        get {
            if (transform.childCount > 0) {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (!Item && gameObject.CompareTag("InventorySlot"))//if slot is empty...
        {
            InventoryDragger.itemBeingDragged.transform.SetParent(transform); //set dragged object's parent to this slot
            Debug.Log(transform + ":A");
        }
        else if (Item)
        {
            Debug.Log("B");
            InventoryDragger.itemBeingDragged.transform.SetParent(transform);
            this.Item.transform.SetParent(StoreOriginalParent.draggedItemParent);
            StoreOriginalParent.draggedItemParent = null;
        }
    }
}
