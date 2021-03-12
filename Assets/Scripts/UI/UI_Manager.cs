using UnityEngine;

public class UI_Manager<T> : MonoBehaviour where T : UI_Item 
{
    protected T[] items;
    protected T selectedItem;

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
        selectedItem = items[index];
        for(int i = 0; i < items.Length; i++) items[i].Selected = (i == index);
    }

    protected void UpdateItemsAvailability(int lastAvailableItemIndex)
    {
        for(int i = 0; i < items.Length; i++) items[i].IsAvailable = i <= lastAvailableItemIndex;
    }
}