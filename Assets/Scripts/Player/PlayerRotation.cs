using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public bool IsAiming => animator.GetBool("Aiming");

    [SerializeField] private Transform rotateBone = null;
    [SerializeField] private GameObject extinguishButton = null;
    [SerializeField] private GameObject extinguisherHose = null;
    [SerializeField] private GameObject extinguisherHoseHidden = null;

    private PlayerMovement movement;
    private Animator animator;

    public void ResetRotation() => rotateBone.localRotation = Quaternion.Euler(0, 0, 0);

    public void StartAiming()
    {
        if(!movement.IsMoving)
        {
            extinguishButton.SetActive(true);
            extinguisherHose.SetActive(true);
            extinguisherHoseHidden.SetActive(false);
            animator.SetBool("Aiming", true);
        }
    }

    public void StopAiming()
    {
        extinguishButton.SetActive(false);
        extinguisherHose.SetActive(false);
        extinguisherHoseHidden.SetActive(true);
        animator.SetBool("Aiming", false);
        ResetRotation();
    }

    public void UpdateRotation()
    {
        if(IsAiming)
        {
            Vector3 newRotation = rotateBone.localEulerAngles;
            newRotation.z += ScreenEventsHandler.DragDelta.y;
            rotateBone.localEulerAngles = newRotation;
            ClampRotation(-45, 60);  
        }
    }

    private void Awake() 
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();    
        StopAiming();
    }

    private void OnEnable() => ScreenEventsHandler.ScreenDragged += UpdateRotation;

    private void OnDisable() => ScreenEventsHandler.ScreenDragged -= UpdateRotation;

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