using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenEventsHandler : MonoBehaviour, IDragHandler
{
    public static event System.Action ScreenDragged;
    public static Vector2 DragDelta { get; private set; }

    public void OnDrag(PointerEventData eventData)
    {
        DragDelta = eventData.delta;       
        ScreenDragged?.Invoke(); 
    }
}