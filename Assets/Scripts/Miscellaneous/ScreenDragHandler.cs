using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenDragHandler : MonoBehaviour, IDragHandler
{
    public event System.Action Dragged;
    public Vector2 Delta { get; private set; }

    public void OnDrag(PointerEventData eventData)
    {
        Delta = eventData.delta;       
        Dragged?.Invoke(); 
    }
}