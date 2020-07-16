using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform rotateBone = null;
    [SerializeField] private ScreenDragHandler screenDragHandler = null;

    private PlayerMovement movement;

    public void ResetRotation() => rotateBone.localRotation = Quaternion.Euler(0, 0, 0);

    private void Awake() 
    {
        movement = GetComponent<PlayerMovement>();    
    }

    private void OnEnable() 
    {
        screenDragHandler.Dragged += UpdateRotation;    
    }

    private void OnDisable() 
    {
        screenDragHandler.Dragged -= UpdateRotation;
    }

    private void UpdateRotation()
    {
        if(!movement.IsMoving)
        {
            Vector3 newRotation = rotateBone.localEulerAngles;
            newRotation.z += screenDragHandler.Delta.y;
            rotateBone.localEulerAngles = newRotation;
            ClampRotation(-45, 60);   
        }
    }

    private void ClampRotation(float minRotation, float maxRotation)
    {
        Vector3 convertedRotation = rotateBone.localEulerAngles;
        if(rotateBone.localEulerAngles.z >= 180) convertedRotation.z -= 360;
        if(convertedRotation.z > maxRotation) 
        {
            convertedRotation.z = maxRotation;
            rotateBone.localEulerAngles = convertedRotation;
        }
        else if(convertedRotation.z < minRotation)
        {
            convertedRotation.z = minRotation;
            rotateBone.localEulerAngles = convertedRotation;
        }
    } 
}