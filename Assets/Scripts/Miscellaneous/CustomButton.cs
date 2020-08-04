using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler
{
    public event System.Action PointerDown;
    public bool Hold { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown?.Invoke();
        Hold = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Hold = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hold = false;
    }
}