using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class UI_Item : MonoBehaviour, IPointerClickHandler
{
    public event System.Action<int> Clicked;
    public bool Selected
    {
        get => selected;
        set
        {
            selected = value;
            outline.enabled = selected;
        }
    }
    public int Index { get; protected set; }

    private bool selected;
    private Outline outline;

    protected virtual void Awake() 
    {
        outline = GetComponent<Outline>();
        Selected = false;
    }

    public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(Index);
}