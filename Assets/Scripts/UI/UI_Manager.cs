using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager<T> : MonoBehaviour where T : UI_Item 
{
    public T SelectedItem => items[selectedItemIndex];

    [SerializeField] protected T item = null;
    [SerializeField] protected RectTransform itemsParent = null;

    protected List<T> items = new List<T>();
    protected int selectedItemIndex;

    protected virtual void OnEnable() 
    {
        StartCoroutine(MakeSelectedItemVisible());
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

    private IEnumerator MakeSelectedItemVisible()
    {
        yield return new WaitForEndOfFrame();
        float newXPos = -Mathf.Lerp(0, itemsParent.sizeDelta.x, (float)selectedItemIndex / (items.Count - 1));
        itemsParent.anchoredPosition = new Vector2(newXPos, itemsParent.anchoredPosition.y);
    }
}