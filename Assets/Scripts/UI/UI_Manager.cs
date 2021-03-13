using UnityEngine;

public class UI_Manager<T> : MonoBehaviour where T : UI_Item 
{
    public T SelectedItem => items[selectedItemIndex];
    public int LastAvailableItemIndex
    {
        get => lastAvailableItemIndex;
        protected set
        {
            value = Mathf.Clamp(value, 0, items.Length - 1);
            lastAvailableItemIndex = value;
            UpdateItemsAvailability();
        }
    }

    protected T[] items;
    protected int selectedItemIndex;

    private int lastAvailableItemIndex;

    protected virtual void OnEnable() 
    {
        for(int i = 0; i < items.Length; i++) items[i].Clicked += SelectItem;
    }

    protected virtual void OnDisable() 
    {
        for(int i = 0; i < items.Length; i++) items[i].Clicked -= SelectItem;
    }

    protected virtual void SelectItem(int index)
    {
        selectedItemIndex = index;
        for(int i = 0; i < items.Length; i++) items[i].Selected = (i == index);
    }

    private void UpdateItemsAvailability()
    {
        for(int i = 0; i < items.Length; i++) items[i].IsAvailable = i <= LastAvailableItemIndex;
    }
}