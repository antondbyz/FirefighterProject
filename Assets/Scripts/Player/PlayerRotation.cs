using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public bool IsAiming => animator.GetBool("Aiming");

    [SerializeField] private Transform rotateBone = null;
    [SerializeField] private ScreenEventsHandler screenEventsHandler = null;

    private PlayerMovement movement;
    private Animator animator;

    public void ResetRotation() => rotateBone.localRotation = Quaternion.Euler(0, 0, 0);

    private void Awake() 
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();    
    }

    private void OnEnable() 
    {
        screenEventsHandler.Touched += PlayAimAnimation;
        screenEventsHandler.Dragged += UpdateRotation;  
        screenEventsHandler.Dragged += PlayAimAnimation;
        screenEventsHandler.Untouched += StopAimAnimation;  
    }

    private void OnDisable() 
    {
        screenEventsHandler.Touched -= PlayAimAnimation;
        screenEventsHandler.Dragged -= UpdateRotation;
        screenEventsHandler.Dragged -= PlayAimAnimation;
        screenEventsHandler.Untouched -= StopAimAnimation;
    }

    private void UpdateRotation()
    {
        if(!movement.IsMoving)
        {
            Vector3 newRotation = rotateBone.localEulerAngles;
            newRotation.z += screenEventsHandler.Delta.y;
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

    private void PlayAimAnimation() 
    {
        if(!movement.IsMoving) animator.SetBool("Aiming", true);
    }

    private void StopAimAnimation()
    { 
        animator.SetBool("Aiming", false);
        ResetRotation();
    }
}