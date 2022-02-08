using UnityEngine;

public class DisableElectricity : MonoBehaviour
{
    [SerializeField] private GameObject[] destroyObjects = new GameObject[0];
    [SerializeField] private Component[] destroyComponents = new Component[0];

    private void OnEnable()
    {
        DeEnergizeController.Deenergize += OnDeenergize;
    }

    private void OnDisable()
    {
        DeEnergizeController.Deenergize -= OnDeenergize;
    }

    private void OnDeenergize()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        for(int i = 0; i < destroyObjects.Length; i++)
            Destroy(destroyObjects[i]);
        for(int i = 0; i < destroyComponents.Length; i++)
            Destroy(destroyComponents[i]);
    }
}