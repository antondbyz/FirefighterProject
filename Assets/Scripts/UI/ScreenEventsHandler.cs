using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenEventsHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static event System.Action PointerDown;
    public static event System.Action PointerUp;
    public static event System.Action Drag;
    public static Vector2 DragDelta { get; private set; }

    [SerializeField] private Camera mainCamera = null;

    private Vector2 previousPointerPos;

    public void OnDrag(PointerEventData eventData)
    {   
        Vector2 newPos = mainCamera.ScreenToWorldPoint(eventData.position);
        DragDelta = newPos - previousPointerPos;  
        previousPointerPos = newPos;
        Drag?.Invoke(); 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        previousPointerPos = mainCamera.ScreenToWorldPoint(eventData.position);
        PointerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp?.Invoke();
    }
}