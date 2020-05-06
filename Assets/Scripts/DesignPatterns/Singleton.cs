using UnityEngine;

/// Makes the inherited class a singleton.
/// Only the type of the inherited class can be passed as the T parameter.
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    
    protected virtual void Awake() 
    {
        if(Instance == null) Instance = this as T;
        else Destroy(this);
    }
}