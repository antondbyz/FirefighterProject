using UnityEngine;

public class PlayerDoorsUnlocker : MonoBehaviour
{
    [SerializeField] private GameObject openButton = null;
    [SerializeField] private GameObject needKeyMessage = null; 

    private Transform myTransform;
    private LockedDoor currentLockedDoor;

    private void Awake() 
    {
        myTransform = transform; 
        openButton.SetActive(false);   
    }

    private void OnDisable() => currentLockedDoor = null;

    private void Update() 
    {
        if(currentLockedDoor != null)
        {
            if(currentLockedDoor.CanBeUnlocked)
            {
                if(!openButton.activeSelf) openButton.SetActive(true);
                if(InputManager.OpenPressed && currentLockedDoor.TryUnlock(myTransform.position.x)) 
                    currentLockedDoor = null;
            }
            else if(!needKeyMessage.activeSelf) needKeyMessage.SetActive(true);
        }
        else 
        {
            if(openButton.activeSelf) openButton.SetActive(false);
            if(needKeyMessage.activeSelf) needKeyMessage.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {  
        if(other.CompareTag("UnlockZone")) currentLockedDoor = other.GetComponentInParent<LockedDoor>();
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("UnlockZone")) currentLockedDoor = null;
    }
}