﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragger : MonoBehaviour, IPointerDownHandler, IDragHandler {

    protected Vector2 pointerOffset; //offset mouse drag position so it doesn't always center when clicked
    protected RectTransform canvasRectTransform; //canvas transform
    protected RectTransform panelRectTransform; //panel to be dragged
    protected Canvas canvas; //whole canvas

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvasRectTransform = canvas.transform as RectTransform;
            panelRectTransform = transform.parent as RectTransform;
        }
    }

    public void OnPointerDown(PointerEventData data) {
        //panelRectTransform.SetAsLastSibling();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, out pointerOffset);
    }

    public void OnDrag(PointerEventData data)
    {
        if (panelRectTransform == null)
            return;

        Vector2 pointerPosition = ClampToWindow(data);
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform, pointerPosition, data.pressEventCamera, out localPointerPosition));
        { panelRectTransform.localPosition = localPointerPosition - pointerOffset; }
    }

    private Vector2 ClampToWindow(PointerEventData data) {
        Vector2 rawPointerPosition = data.position;
        Vector3[] canvasCorners = new Vector3[4];
        canvasRectTransform.GetWorldCorners(canvasCorners);
        float clampedX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
        float clampedY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

        Vector2 newPointerPosition = new Vector2(clampedX, clampedY);
        return newPointerPosition;
    }
}
