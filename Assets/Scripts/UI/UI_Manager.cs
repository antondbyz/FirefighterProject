using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager<T> : MonoBehaviour where T : UI_Item 
{
    public T SelectedItem => items[selectedItemIndex];

    [SerializeField] protected T item = null;
    [SerializeField] protected RectTransform itemsParent = null;
    [SerializeField] protected AudioClip selectItemClip = null;

    protected List<T> items = new List<T>();
    protected int selectedItemIndex;

    private float canvasReferenceWidth;

    protected virtual void Awake()
    {
        canvasReferenceWidth = transform.root.GetComponent<CanvasScaler>().referenceResolution.x;
    }

    protected virtual void OnEnable() 
    {
        StartCoroutine(MakeSelectedItemVisible());
        for(int i = 0; i < items.Count; i++) items[i].Clicked += SelectItem;
    }

    protected virtual void OnDisable() 
    {
        for(int i = 0; i < items.Count; i++) items[i].Clicked -= SelectItem;
    }

    protected virtual void SelectItem(int index, bool playSound)
    {
        selectedItemIndex = index;
        for(int i = 0; i < items.Count; i++) 
        {
            if(i == index)
            {
                if(playSound && !items[i].Selected) AudioManager.Instance.PlayClip(selectItemClip);
                items[i].Selected = true;
            }
            else items[i].Selected = false;
        }
    }

    protected void UpdateItemsAvailability(int lastAvailableItemIndex)
    {
        lastAvailableItemIndex = Mathf.Clamp(lastAvailableItemIndex, 0, items.Count - 1);
        for(int i = 0; i < items.Count; i++) items[i].IsAvailable = i <= lastAvailableItemIndex;
    }

    private IEnumerator MakeSelectedItemVisible()
    {
        yield return new WaitForEndOfFrame();
        float newXPos = -(SelectedItem.transform.localPosition.x - canvasReferenceWidth / 2);
        itemsParent.anchoredPosition = new Vector2(newXPos, itemsParent.anchoredPosition.y);
    }
}