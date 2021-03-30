using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayClipOnButtonClick : MonoBehaviour
{
    [SerializeField] private AudioClip clip = null;

    private void Awake() 
    {
        GetComponent<Button>().onClick.AddListener(PlayClip);
    }

    private void PlayClip() => AudioManager.Instance.PlayClip(clip);
}