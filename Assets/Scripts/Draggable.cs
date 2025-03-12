using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public event Action OnDragEnd;

    public new Transform transform;
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging = false;

    public void StartDragging()
    {
        isDragging = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    public void StopDragging()
    {
        isDragging = false;
        OnDragEnd.Invoke();
    }

    void OnMouseDown()
    {
        StartDragging();
    }

    void Update()
    {
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
        if (!isDragging) return;
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }

    void OnMouseUp()
    {
        StopDragging();
    }
}
