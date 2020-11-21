using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event System.Action Pressed;
    public bool Held { get; private set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Pressed?.Invoke();
        Held = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Held = false;
    }

    private void OnDisable() => Held = false;
}