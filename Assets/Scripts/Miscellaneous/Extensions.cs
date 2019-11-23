using System.Collections;
using UnityEngine;

public static class Extensions
{
    public static IEnumerator SmoothlyChangeColor(this Material material, WaitForSeconds delay, Color endColor, float time)
    {
        Color c = material.color;
        float rDelta = 0.1f / time * (Mathf.Abs(c.r - endColor.r));
        float gDelta = 0.1f / time * (Mathf.Abs(c.g - endColor.g));
        float bDelta = 0.1f / time * (Mathf.Abs(c.b - endColor.b));
        while(material.color != endColor)
        {
            c.r = Mathf.MoveTowards(c.r, endColor.r, rDelta);
            c.g = Mathf.MoveTowards(c.g, endColor.g, gDelta);
            c.b = Mathf.MoveTowards(c.b, endColor.b, bDelta);
            material.color = c;
            yield return delay;
        }
    }

    // Returns the percentage of a number ranging from minValue to maxValue
    public static float NumberToPercentage(this float value, float minValue, float maxValue)
    {
        if(value > maxValue || value < minValue) return -1;
        return Mathf.Abs((value - minValue) / (maxValue - minValue));
    }

    
    // Returns a number ranging from minValue to maxValue converted from percentage
    public static float PercentageToNumber(this float percentage, float minValue, float maxValue)
    {
        if(percentage > 1 || percentage < 0) return -1;
        return minValue + (maxValue - minValue) * percentage;
    }
}
