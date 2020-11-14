using UnityEngine;

public static class Helper
{
    private static ContactFilter2D filter = new ContactFilter2D();    

    public static RaycastHit2D IgnoreTriggersRaycast(Vector2 origin, Vector2 direction, float distance, int layerMask = -1)
    {
        filter.useTriggers = false;
        filter.layerMask = layerMask;
        RaycastHit2D[] hit = new RaycastHit2D[1];
        Physics2D.Raycast(origin, direction, filter, hit, distance);
        return hit[0];
    }

    public static RaycastHit2D IgnoreTriggersBoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance,
                                                     int layerMask = -1)
    {
        filter.useTriggers = false;
        filter.layerMask = layerMask;
        RaycastHit2D[] hit = new RaycastHit2D[1];
        Physics2D.BoxCast(origin, size, angle, direction, filter, hit, distance);
        return hit[0];
    }
}
