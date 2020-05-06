using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenTouchesHandler : Singleton<ScreenTouchesHandler>, IDragHandler, IPointerDownHandler
{
    public event System.Action ScreenTouched;
    public Vector2 WorldTouchPosition { get; private set; }
    private Camera mainCamera;

    protected override void Awake() 
    {
        base.Awake();
        mainCamera = Camera.main;    
    }

    public void OnDrag(PointerEventData eventData)
    {
        WorldTouchPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        ScreenTouched?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        WorldTouchPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        ScreenTouched?.Invoke();
    }
}