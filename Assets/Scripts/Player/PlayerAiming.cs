using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public static event System.Action StoppedAiming;
    public static bool IsAiming { get; private set; }

    [SerializeField] private Transform rotateBone = null;
    [SerializeField] private GameObject extinguishButton = null;
    [SerializeField] private GameObject extinguisherHose = null;
    [SerializeField] private GameObject extinguisherHoseHidden = null;

    private PlayerController controller;
    private Animator animator;

    public void ResetRotation() => rotateBone.localRotation = Quaternion.Euler(0, 0, 0);

    public void StartAiming()
    {
        if(!controller.IsMoving)
        {
            IsAiming = true;
            extinguishButton.SetActive(true);
            extinguisherHose.SetActive(true);
            extinguisherHoseHidden.SetActive(false);
            animator.SetBool("Aiming", true);
        }
    }

    public void StopAiming()
    {
        IsAiming = false;
        extinguishButton.SetActive(false);
        extinguisherHose.SetActive(false);
        extinguisherHoseHidden.SetActive(true);
        animator.SetBool("Aiming", false);
        ResetRotation();
        StoppedAiming?.Invoke();
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
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();    
        StopAiming();
    }

    private void OnEnable()
    { 
        ScreenEventsHandler.PointerDown += StartAiming;
        ScreenEventsHandler.Drag += StartAiming;
        ScreenEventsHandler.Drag += UpdateRotation;
        ScreenEventsHandler.PointerUp += StopAiming;
    }

    private void OnDisable() 
    { 
        ScreenEventsHandler.PointerDown -= StartAiming;
        ScreenEventsHandler.Drag -= StartAiming;
        ScreenEventsHandler.Drag -= UpdateRotation;
        ScreenEventsHandler.PointerUp -= StopAiming;
    }

    private void ClampRotation(float minRotation, float maxRotation)
    {
        Vector3 convertedRotation = rotateBone.localEulerAngles;
        if(rotateBone.localEulerAngles.z >= 180) convertedRotation.z -= 360;
        convertedRotation.z = Mathf.Clamp(convertedRotation.z, minRotation, maxRotation);
        rotateBone.localEulerAngles = convertedRotation;
    } 
}