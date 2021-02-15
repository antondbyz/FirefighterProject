using UnityEngine;

public class PlayerUnlockDoors : MonoBehaviour
{
    private Transform myTransform;
    private LockedDoor currentLockedDoor;
    private string unlockZoneTag = "UnlockDoorZone";

    private void Awake() 
    {
        myTransform = transform;    
    }

    private void Update() 
    {
        if(currentLockedDoor != null && InputManager.OpenPressed) currentLockedDoor.TryUnlock();    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Key")) Destroy(other.gameObject);    
        else if(other.CompareTag(unlockZoneTag)) currentLockedDoor = other.GetComponent<LockedDoor>();
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag(unlockZoneTag)) currentLockedDoor = null;
    }
}