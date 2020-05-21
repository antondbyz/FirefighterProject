using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    private Transform myTransform;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        playerMovement = myTransform.parent.GetComponent<PlayerMovement>();
    }
}