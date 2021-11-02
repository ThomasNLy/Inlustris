using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static GameObject itemBeingDragged; //static because only 1 object dragged at a time *global item*
    public GameObject tempSlotForDraggedObject;
    Vector3 startPosition; // original position of dragged object in case it gets dragged in illegal spot
    Transform startParent; // transform of new position

    private void Start()
    {
        tempSlotForDraggedObject = GameObject.FindGameObjectWithTag("Inventory");
    }

    public void OnBeginDrag(PointerEventData eventData) //start of click *only concerns clicked object*
    {
        itemBeingDragged = gameObject;        
        tempSlotForDraggedObject = GameObject.Find("CurrentlyDragged"); //this forces the object to be in front
        startPosition = transform.position;
        startParent = transform.parent;
        StoreOriginalParent.draggedItemParent = startParent; //original slot
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) //holding click
    {
        transform.position = Input.mousePosition; //dragged object follows cursor
        transform.SetParent(tempSlotForDraggedObject.transform); //set parent to CurrentlyDragged temp slot
        transform.gameObject.tag = "CurrentlyDraggedItem";
    }

    public void OnEndDrag(PointerEventData eventData) //release click
    {
        itemBeingDragged = null;

        if (transform.position != startPosition)
        {
            transform.position = startPosition;
            Debug.Log("Slot");
 
        }
        
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        
    }
}
