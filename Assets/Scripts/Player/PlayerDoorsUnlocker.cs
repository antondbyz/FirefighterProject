using UnityEngine;

public class PlayerDoorsUnlocker : MonoBehaviour
{
    [SerializeField] private GameObject openButton = null;

    private Transform myTransform;
    private LockedDoor currentLockedDoor;

    private void Awake() 
    {
        myTransform = transform; 
        openButton.SetActive(false);   
    }

    private void Update() 
    {
        if(currentLockedDoor != null)
        {
            if(!openButton.activeSelf) openButton.SetActive(true);
            if(InputManager.OpenPressed) currentLockedDoor.TryUnlock();    
        }
        else if(openButton.activeSelf) openButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Key")) Destroy(other.gameObject);    
        else if(other.CompareTag("UnlockZone"))
        {
            currentLockedDoor = other.GetComponentInParent<LockedDoor>();
            currentLockedDoor.Unlocked += ResetCurrentLockedDoor;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("UnlockZone")) 
        {
            currentLockedDoor.Unlocked -= ResetCurrentLockedDoor;
            currentLockedDoor = null;
        }
    }

    private void ResetCurrentLockedDoor() => currentLockedDoor = null;
}