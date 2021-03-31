using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline), typeof(Button))]
public class UI_Item : MonoBehaviour
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
    public virtual bool IsAvailable
    {
        get => isAvailable;
        set => isAvailable = value; 
    }
    public int Index { get; protected set; }

    protected bool isAvailable;

    private bool selected;
    private Outline outline;

    protected virtual void Awake() 
    {
        GetComponent<Button>().onClick.AddListener(InvokeClicked);
        outline = GetComponent<Outline>();
        Selected = false;
    }

    private void InvokeClicked() => Clicked?.Invoke(Index);
}