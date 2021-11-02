using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCaller : CloseWindow {

    GameObject inventoryPanel;

    private void Awake()
    {
        inventoryPanel = GameObject.FindGameObjectWithTag("Inventory");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            OpenClose(inventoryPanel);
    }

    public GameObject GetInventoryPanel() {
        return inventoryPanel;
    }
}
