using UnityEngine;

public class DisableElectricity : MonoBehaviour
{
    [SerializeField] private GameObject[] destroyObjects = new GameObject[0];
    [SerializeField] private Component[] destroyComponents = new Component[0];

    private void Awake()
    {
        GameController.Instance.ElectricityDisabled += TurnOff;
    }

    private void TurnOff()
    {
        for(int i = 0; i < destroyObjects.Length; i++)
            Destroy(destroyObjects[i]);
        for(int i = 0; i < destroyComponents.Length; i++)
            Destroy(destroyComponents[i]);
    }
}