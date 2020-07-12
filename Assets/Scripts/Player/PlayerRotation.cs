using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform rotateBone = null;
    [SerializeField] private ScreenTouchesHandler screenTouchesHandler = null;

    private PlayerMovement movement;

    public void UpdateDirection()
    {
        LookAtTouchPosition();
        ClampRotation(-45, 60);
    }

    private void Awake() 
    {
        movement = GetComponent<PlayerMovement>();   
    }

    private void LookAtTouchPosition()
    {
        Vector2 direction = screenTouchesHandler.WorldTouchPosition - (Vector2)rotateBone.position;
        Vector2 convertedDirection = Quaternion.Euler(0, 0, 180) * direction;
        convertedDirection.x = movement.FlipX ? 1 : -1;
        rotateBone.rotation = Quaternion.LookRotation(rotateBone.forward, convertedDirection);
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

    private void OnEnable() 
    {
        screenTouchesHandler.ScreenTouched += UpdateDirection;    
    }

    private void OnDisable() 
    {
        screenTouchesHandler.ScreenTouched -= UpdateDirection;
    }
}