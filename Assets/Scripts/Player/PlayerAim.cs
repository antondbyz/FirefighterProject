using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public bool IsAiming { get; private set; }

    [SerializeField] private Transform rotateBone = null;
    [SerializeField] private GameObject extinguisherHose = null;
    [SerializeField] private GameObject extinguisherHoseHidden = null;

    private PlayerController controller;

    public void ResetRotation() => rotateBone.localRotation = Quaternion.Euler(0, 0, 0);

    private void Awake() 
    {
        controller = GetComponent<PlayerController>(); 
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
        StopAiming();
    }

    private void StartAiming()
    {
        if(!controller.IsMoving && controller.IsGrounded)
        {
            IsAiming = true;
            extinguisherHose.SetActive(true);
            extinguisherHoseHidden.SetActive(false);
        }
    }

    private void StopAiming()
    {
        IsAiming = false;
        extinguisherHose.SetActive(false);
        extinguisherHoseHidden.SetActive(true);
        ResetRotation();
    }

    private void UpdateRotation()
    {
        if(IsAiming)
        {
            Vector3 newRotation = rotateBone.localEulerAngles;
            newRotation.z += ScreenEventsHandler.DragDelta.y;
            rotateBone.localEulerAngles = newRotation;
            ClampRotation(-45, 60);  
        }
    }

    private void ClampRotation(float minRotation, float maxRotation)
    {
        Vector3 convertedRotation = rotateBone.localEulerAngles;
        if(rotateBone.localEulerAngles.z >= 180) convertedRotation.z -= 360;
        convertedRotation.z = Mathf.Clamp(convertedRotation.z, minRotation, maxRotation);
        rotateBone.localEulerAngles = convertedRotation;
    } 
}