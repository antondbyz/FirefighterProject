using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public static TouchHandler Instance;
    public Vector2 WorldTouchPosition { get; private set; }
    [SerializeField] private UnityEvent onDrag = new UnityEvent();
    [SerializeField] private UnityEvent onPointerDown = new UnityEvent();
    private Camera mainCamera;

    private void Awake() 
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();    
        if(Instance == null) Instance = this;
        else if(Instance == this) Destroy(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        WorldTouchPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        onDrag.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        WorldTouchPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        onPointerDown.Invoke();
    }
}