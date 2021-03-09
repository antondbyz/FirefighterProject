using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class Selectable : MonoBehaviour, IPointerClickHandler
{
    public event System.Action<Selectable> Clicked;
    public bool Selected
    {
        get => selected;
        set
        {
            selected = value;
            outline.enabled = selected;
        }
    }

    private bool selected;
    private Outline outline;

    protected virtual void Awake() 
    {
        outline = GetComponent<Outline>();
    }

    public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(this);
}