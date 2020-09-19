using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
    private GameObject playerCharacterGO;
    private PlayerLifes playerLifes;
    private PlayerCharacter playerCharacter;

    public void PauseLevel() => GameController.Instance.PauseLevel();

    private void Awake() 
    {
        playerCharacterGO = transform.GetChild(0).gameObject;
        playerLifes = playerCharacterGO.GetComponent<PlayerLifes>();
        playerCharacter = playerCharacterGO.GetComponent<PlayerCharacter>();
    }

    private void OnEnable() => playerLifes.Died += PlayerDied;

    private void OnDisable() => playerLifes.Died -= PlayerDied;

    private void PlayerDied()
    {
        playerCharacterGO.SetActive(false);
        if(playerLifes.LifesLeft > 0)
        {
            StartCoroutine(MovePlayerToCurrentCheckpoint());
        }
        else
        { 
            StartCoroutine(GameController.Instance.FailLevel(1));
        }
    }

    private IEnumerator MovePlayerToCurrentCheckpoint()
    {
        yield return new WaitForSeconds(1);
        playerCharacter.MoveToCurrentCheckpoint();
        playerCharacterGO.SetActive(true);
    }
}