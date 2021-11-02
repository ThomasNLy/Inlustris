using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSlot : MonoBehaviour
{

    [SerializeField]
    private float gapBetween;

    [SerializeField]
    private int numberOfSlots;

    [SerializeField]
    private GameObject InventorySlot;

    private void Start()
    {
        addSlot(numberOfSlots, gapBetween);
    }

    public void addSlot(int numberOfSlots, float gapBetween)
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject g = Instantiate(InventorySlot);
            g.transform.parent = transform;
            g.transform.position = transform.position;

            g.GetComponent<Image>().color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        }
    }
}
