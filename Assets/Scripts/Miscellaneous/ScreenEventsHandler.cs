using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenEventsHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static event System.Action PointerDown;
    public static event System.Action PointerUp;
    public static event System.Action Drag;
    public static Vector2 DragDelta { get; private set; }

    public void OnDrag(PointerEventData eventData)
    {
        DragDelta = eventData.delta;       
        Drag?.Invoke(); 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp?.Invoke();
    }
}