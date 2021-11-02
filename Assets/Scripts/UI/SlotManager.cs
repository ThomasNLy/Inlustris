using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{

    [SerializeField]
    GameObject inventorySlot;

    [SerializeField]
    private int numberOfSlotsToAdd;

    private int currentSlotPos = 0;

    private void Awake()
    {
        for (int i = 0; i < numberOfSlotsToAdd; i++)
            AddSlot(i + 1);
    }

    private void Start()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void AddSlot(int slotNum)
    {
        GameObject newSlot = Instantiate(inventorySlot);
        newSlot.name = "Slot" + slotNum;
        newSlot.transform.SetParent(transform);
        GridLayoutGroup grid = newSlot.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(50, 50);
    }

    public GameObject FirstEmptySlot()
    {
        if (transform.GetChild(currentSlotPos).childCount > 0)//if child of the current empty slot is occupied
        {
            if (!IsLastSlot())
            {
                currentSlotPos++;
                return FirstEmptySlot();
            }
            else
                return FirstEmptySlot();
        }
        else
        {
            int emptyPos = currentSlotPos;
            currentSlotPos = 0;
            return transform.GetChild(emptyPos).gameObject;
        }
    }

    public bool IsLastSlot()
    {
        return currentSlotPos == numberOfSlotsToAdd;
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount > 0)
                return false;
        }
        return true;
    }

    public bool IsFull()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount == 0)
                return false;
        }
        return true;
    }
}
