using System.Collections.Generic;
using UnityEngine;

public class UI_Manager<T> : MonoBehaviour where T : UI_Item 
{
    public T SelectedItem => items[selectedItemIndex];

    protected List<T> items = new List<T>();
    protected int selectedItemIndex;

    protected virtual void OnEnable() 
    {
        for(int i = 0; i < items.Count; i++) items[i].Clicked += SelectItem;
    }

    protected virtual void OnDisable() 
    {
        for(int i = 0; i < items.Count; i++) items[i].Clicked -= SelectItem;
    }

    protected virtual void SelectItem(int index)
    {
        selectedItemIndex = index;
        for(int i = 0; i < items.Count; i++) items[i].Selected = (i == index);
    }

    protected void UpdateItemsAvailability(int lastAvailableItemIndex)
    {
        lastAvailableItemIndex = Mathf.Clamp(lastAvailableItemIndex, 0, items.Count - 1);
        for(int i = 0; i < items.Count; i++) items[i].IsAvailable = i <= lastAvailableItemIndex;
    }
}