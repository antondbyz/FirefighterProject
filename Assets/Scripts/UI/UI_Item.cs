using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class UI_Item : MonoBehaviour, IPointerClickHandler
{
    public event System.Action<int, bool> Clicked;
    public bool Selected
    {
        get => selected;
        set
        {
            selected = value;
            outline.enabled = selected;
        }
    }
    public virtual bool IsAvailable
    {
        get => isAvailable;
        set => isAvailable = value; 
    }
    public int Index { get; protected set; }

    protected bool isAvailable;

    private bool selected;
    private Outline outline;

    public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(Index, true);

    protected virtual void Awake() 
    {
        outline = GetComponent<Outline>();
        Selected = false;
    }
}