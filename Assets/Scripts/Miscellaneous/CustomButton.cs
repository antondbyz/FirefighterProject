using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event System.Action Pressed;
    public bool Hold { get; private set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Pressed?.Invoke();
        Hold = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hold = false;
    }

    private void OnDisable() 
    {
        Hold = false;   
    }
}