using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGUI : MonoBehaviour {

    public int selGridInt = 0;
    public string[] selStrings = new string[] { "Grid 1", "Grid 2", "Grid 3", "Grid 4" };

    void OnGUI()
    {
        // use 2 elements in the horizontal direction
        selGridInt = GUI.SelectionGrid(new Rect(0, 0, 300, 90), selGridInt, selStrings, 2);
    }
}
