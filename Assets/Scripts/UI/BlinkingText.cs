using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour 
{
    [SerializeField] private float blinkSpeed = 1;
    [SerializeField] private bool alwaysBlink = false;
    
    private TMP_Text myText;
    private float t = 1;
    private float newAlpha;
    private Color newColor;
    private bool reverseTimer = true;
    private int blinkTimes;
    private int blinksCounter;

    private void Awake() 
    {
        myText = GetComponent<TMP_Text>();    
    }

    private void Update() 
    {
        if(alwaysBlink || blinksCounter < blinkTimes)
        {
            if(t < 0) reverseTimer = false;
            else if(t > 1)
            { 
                reverseTimer = true;
                blinksCounter++;
            }
            if(reverseTimer) t -= Time.unscaledDeltaTime * blinkSpeed;
            else t += Time.unscaledDeltaTime * blinkSpeed;
            newAlpha = Mathf.Lerp(0, 1, t);
            newColor = myText.color;
            newColor.a = newAlpha;
            myText.color = newColor;
        }
    }

    public void Blink(int times)
    {
        blinkTimes = times;
        blinksCounter = 0;
    }
}