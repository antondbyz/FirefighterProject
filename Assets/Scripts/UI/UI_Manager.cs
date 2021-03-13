using UnityEngine;

public class UI_Manager<T> : MonoBehaviour where T : UI_Item 
{
    public T SelectedItem => items[selectedItemIndex];

    protected T[] items;
    protected int selectedItemIndex;
    protected int lastAvailableItemIndex;

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

    protected void UpdateItemsAvailability(int lastAvailableItemIndex) 
    {
        this.lastAvailableItemIndex = lastAvailableItemIndex;
        UpdateItemsAvailability();
    }

    protected void UpdateItemsAvailability()
    {
        for(int i = 0; i < items.Length; i++) items[i].IsAvailable = i <= lastAvailableItemIndex;
    }
}