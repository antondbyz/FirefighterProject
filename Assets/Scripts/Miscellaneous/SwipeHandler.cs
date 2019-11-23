using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwipeHandler : MonoBehaviour, IDragHandler
{
    public static UnityEvent OnSwipe = new UnityEvent();
    public static Vector2 Delta;

    public void OnDrag(PointerEventData eventData)
    {
        Delta.x = eventData.delta.x;
        Delta.y = eventData.delta.y;
        OnSwipe.Invoke();
    }
}
