using UnityEngine;

public class DisableElectricity : MonoBehaviour
{
    [SerializeField] private GameObject[] destroyObjects = new GameObject[0];
    [SerializeField] private Component[] destroyComponents = new Component[0];

    private Transform myTransform;

    private void Awake()
    {
        AdsManager.Instance.ElectricityDisabled += TurnOff;
        myTransform = transform;
    }

    private void TurnOff()
    {
        myTransform.rotation = Quaternion.Euler(0, 0, 0);
        for(int i = 0; i < destroyObjects.Length; i++)
            Destroy(destroyObjects[i]);
        for(int i = 0; i < destroyComponents.Length; i++)
            Destroy(destroyComponents[i]);
        AdsManager.Instance.ElectricityDisabled -= TurnOff;
    }
}