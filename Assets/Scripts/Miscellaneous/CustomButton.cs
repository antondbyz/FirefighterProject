using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    public bool Pressed { get; private set; }
    public bool Hold { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
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

    private void FixedUpdate() 
    {
        Pressed = false;
    }
}