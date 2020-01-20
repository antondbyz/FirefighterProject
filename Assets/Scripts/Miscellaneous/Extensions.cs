using System;
using System.Collections;
using UnityEngine;

public static class Extensions
{
    // public static IEnumerator SmoothlyChangeColor(this Material material, Color targetColor, float time)
    // {
    //     Color c = material.color;
    //     float rDelta = 0.1f / time * (Mathf.Abs(c.r - targetColor.r));
    //     float gDelta = 0.1f / time * (Mathf.Abs(c.g - targetColor.g));
    //     float bDelta = 0.1f / time * (Mathf.Abs(c.b - targetColor.b));
    //     while(material.color != targetColor)
    //     {
    //         c.r = Mathf.MoveTowards(c.r, targetColor.r, rDelta);
    //         c.g = Mathf.MoveTowards(c.g, targetColor.g, gDelta);
    //         c.b = Mathf.MoveTowards(c.b, targetColor.b, bDelta);
    //         material.color = c;
    //         yield return delay;
    //     }
    // }

    /// Returns the percentage of a number ranging from minValue to maxValue
    public static float NumberInRangeToPercentage(this float value, float minValue, float maxValue)
    {
        if(value >= maxValue) return 100;
        else if(value <= minValue) return 0;
        return Mathf.Abs((value - minValue) / (maxValue - minValue)) * 100;
    }

    
    /// Returns a number ranging from minValue to maxValue converted from percentage
    public static float PercentageToNumberInRange(this float percentage, float minValue, float maxValue)
    {
        if(percentage > 100 || percentage < 0) throw new ArgumentException("Percentage should be in the range from 0 to 100.");
        return minValue + (maxValue - minValue) * percentage / 100;
    }
}