using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public UnityEvent Down;
    public UnityEvent Up;
    public UnityEvent Hold;

    public bool IsHold { get; private set; } = false;

    private void Update()
    {
        if(IsHold)
        {
            Hold.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!GameManager.IsPaused)
        {
            Down.Invoke();
            IsHold = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!GameManager.IsPaused && IsHold)
        {
            Up.Invoke();
            IsHold = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!GameManager.IsPaused && IsHold)
        {
            Up.Invoke();
            IsHold = false;
        }
    }
}
