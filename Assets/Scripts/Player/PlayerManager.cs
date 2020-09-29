using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
    private PlayerLifes lifes;
    private PlayerCharacter character;

    public void PauseLevel() => GameController.Instance.PauseLevel();

    private void Awake() 
    {
        character = transform.GetChild(0).GetComponent<PlayerCharacter>();
        lifes = character.GetComponent<PlayerLifes>();
    }

    private void OnEnable() => lifes.Died += PlayerDied;

    private void OnDisable() => lifes.Died -= PlayerDied;

    private void PlayerDied()
    {
        character.gameObject.SetActive(false);
        if(lifes.LifesLeft > 0)
        {
            StartCoroutine(MoveCharacterToCurrentCheckpoint());
        }
        else
        { 
            StartCoroutine(GameController.Instance.FailLevel(1));
        }
    }

    private IEnumerator MoveCharacterToCurrentCheckpoint()
    {
        yield return new WaitForSeconds(1);
        character.MoveToCurrentCheckpoint();
        character.gameObject.SetActive(true);
    }
}