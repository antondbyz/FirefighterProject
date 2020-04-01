using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void Start() 
    {
        Instantiate(PlayerManager.Instance.CurrentLevel.Prefab, transform.position, Quaternion.identity);
    }
}
