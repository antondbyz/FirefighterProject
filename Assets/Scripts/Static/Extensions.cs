using UnityEngine;

public static class Extensions 
{
    public static bool ContainsLayer(this LayerMask mask, int layer)
    {
        return (mask.value & (1 << layer)) > 0;
    }
}