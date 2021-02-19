using UnityEngine;
using UnityEngine.EventSystems;

public class GameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool Pressed { get; private set; }
    public bool Held { get; private set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Pressed = true;
        Held = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Held = false;
    }

    private void LateUpdate() 
    {
        Pressed = false;    
    }

    private void OnDisable() 
    { 
        Pressed = false;
        Held = false;
    }
}