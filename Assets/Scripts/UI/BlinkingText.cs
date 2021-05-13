using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    [SerializeField] private float maxAlpha = 1;
    [SerializeField] private float minAlpha = 1;
    [SerializeField] private float speed = 1;

    private TMP_Text myText;
    private float currentAlpha = 1;
    private float targetAlpha = 0;

    private void Awake() 
    {
        myText = GetComponent<TMP_Text>();
        currentAlpha = maxAlpha;
        targetAlpha = minAlpha;
    }

    private void Update() 
    {
        if(currentAlpha == targetAlpha) 
        {
            targetAlpha = targetAlpha == maxAlpha ? minAlpha : maxAlpha;
        }
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, speed * Time.deltaTime);
        myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, currentAlpha);    
    }
}