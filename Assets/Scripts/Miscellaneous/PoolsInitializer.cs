using UnityEngine;

public class PoolsInitializer : MonoBehaviour
{
    private void Awake() 
    {
        Transform myTransform = transform;
        Pool[] pools = Resources.LoadAll<Pool>("Pools");
        for(int i = 0; i < pools.Length; i++)
        {
            for(int j = 0; j < pools[i].Size; j++)
            {
                GameObject obj = Instantiate(pools[i].Prefab, myTransform);
                obj.SetActive(false);
                pools[i].Objects.Enqueue(obj);
            }
        }    
    }
}