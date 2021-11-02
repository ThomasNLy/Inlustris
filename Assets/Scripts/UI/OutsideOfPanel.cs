using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideOfPanel : MonoBehaviour {

    private bool clickedOutsideOfPanel;

    public bool ClickedOutsideOfPanel
    {
        get { return clickedOutsideOfPanel; }
        set { clickedOutsideOfPanel = value; }
    }
	
	void Update () {
       if (Input.GetMouseButtonUp(0) &&
             !RectTransformUtility.RectangleContainsScreenPoint(
                 gameObject.GetComponent<RectTransform>(),
                 Input.mousePosition,
                 null))
        {
            if (GameObject.FindGameObjectWithTag("CurrentlyDraggedItem"))
                GameObject.FindGameObjectWithTag("CurrentlyDraggedItem").GetComponent<Droppable>().Drop();
        }
    }
}
