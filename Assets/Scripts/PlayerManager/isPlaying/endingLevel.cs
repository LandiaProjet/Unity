using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class endingLevel : MonoBehaviour
{
    public static endingLevel instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de isPlaying dans la scene");
            return;
        }
        instance = this;
    }

    public void NextLevel()
    {
        MenuManager.instance.CloseMenu("PopupDefeat");
        MenuManager.instance.CloseMenu("PopupVictory");
        isPlaying.instance.stats = Stats.notHere;
        TransitionManager.instance.loadingTransition.startLoadingLevel(1f, false, 1);
        PlayerManager.instance.changePlayer("idle");
        PlayerMovement.instance.setDie(false);
        PlayerMovement.instance.rb.simulated = true;
        MenuManager.instance.OpenMenu("Level", 10);
        StartCoroutine(WaitBeforeRespawnPlayer());
    }

    public void RestartLevel()
    {
        if (PlayerData.getData().health <= 0)
        {
            Popup.instance.openPopup("Alerte", "Vous n'avez pas assez de coeur pour pouvoir relancer une parti ...", 20);
            return;
        }
        MenuManager.instance.CloseMenu("PopupDefeat");
        MenuManager.instance.CloseMenu("PopupVictory");
        PlayerManager.instance.changePlayer("idle");
        PlayerMovement.instance.setDie(false);
        PlayerMovement.instance.rb.simulated = true;
        LevelManager.instance.openLevel(isPlaying.instance.idLevel);
        StartCoroutine(WaitBeforeRespawnPlayer());
    }

    IEnumerator WaitBeforeRespawnPlayer()
    {
        PlayerSpawn.instance.spawnPlayer();
        yield return new WaitForSeconds(0.5f);
        PlayerMovement.instance.rb.AddForce(new Vector2(-10, 2));
    }

    public void Retrylevel()
    {
        MenuManager.instance.CloseMenu("PopupDefeat");
        MenuManager.instance.CloseMenu("PopupVictory");
        StartCoroutine(isPlaying.instance.StartImmunity(5f));
        PlayerMovement.instance.transform.position = isPlaying.instance.lastPoint;
        PlayerMovement.instance.setDie(false);
        PlayerMovement.instance.rb.simulated = true;
        isPlaying.instance.stats = Stats.inGame;
        isPlaying.instance.addHealth(100);
        isPlaying.instance.credit *= 10;
        HudManager.instance.RecoveryGame();
        isPlaying.instance.time += 30;
        PlayerData.getData().AddHealth();
    }

    public void BackToHome()
    {
        MenuManager.instance.CloseMenu("PopupDefeat");
        MenuManager.instance.CloseMenu("PopupVictory");
        isPlaying.instance.stats = Stats.notHere;
        TransitionManager.instance.loadingTransition.startLoadingLevel(1f, false, 1);
        PlayerManager.instance.changePlayer("idle");
        PlayerMovement.instance.setDie(false);
        PlayerMovement.instance.rb.simulated = true;
        MenuManager.instance.OpenMenu("MainMenu", 10);
        StartCoroutine(WaitBeforeRespawnPlayer());
    }
}
