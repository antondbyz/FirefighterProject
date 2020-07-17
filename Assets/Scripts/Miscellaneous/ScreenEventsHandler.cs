using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenEventsHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public event System.Action Untouched;
    public event System.Action Touched;
    public event System.Action Dragged;
    public Vector2 Delta { get; private set; }

    public void OnDrag(PointerEventData eventData)
    {
        Delta = eventData.delta;       
        Dragged?.Invoke(); 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Touched?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Untouched?.Invoke();
    }
}