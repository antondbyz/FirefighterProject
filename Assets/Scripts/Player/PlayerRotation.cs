using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform rotateBone = null;
    [SerializeField] private float minRotation, maxRotation = 0;

    private PlayerMovement movement;

    public void UpdateDirection()
    {
        LookAtTouchPosition();
        ClampRotation();
    }

    private void Awake() 
    {
        movement = GetComponent<PlayerMovement>();    
    }

    private void OnEnable() 
    {
        ScreenTouchesHandler.Instance.ScreenTouched += UpdateDirection;    
    }

    private void OnDisable() 
    {
        ScreenTouchesHandler.Instance.ScreenTouched -= UpdateDirection;
    }

    private void LookAtTouchPosition()
    {
        Vector2 direction = ScreenTouchesHandler.Instance.WorldTouchPosition - (Vector2)rotateBone.position;
        Vector2 convertedDirection = Quaternion.Euler(0, 0, 180) * direction;
        if((convertedDirection.x > 0 && !movement.FlipX) || (convertedDirection.x < 0 && movement.FlipX))
        {
            convertedDirection.x = -convertedDirection.x;
        }
        rotateBone.rotation = Quaternion.LookRotation(rotateBone.forward, convertedDirection);
    }

    private void ClampRotation()
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