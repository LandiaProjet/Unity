using UnityEngine;
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
        TransitionManager.instance.loadingTransition.startLoading(1f);
        PlayerManager.instance.changePlayer("idle");
        PlayerMovement.instance.setDie(false);
        PlayerMovement.instance.rb.simulated = true;
        SceneManager.LoadScene(1);
        MenuManager.instance.OpenMenu("Level", 10);
    }

    public void RestartLevel()
    {
        MenuManager.instance.CloseMenu("PopupDefeat");
        MenuManager.instance.CloseMenu("PopupVictory");
        PlayerManager.instance.changePlayer("idle");
        PlayerMovement.instance.setDie(false);
        PlayerMovement.instance.rb.simulated = true;
        PlayerSpawn.instance.spawnPlayer();
        LevelManager.instance.openLevel(isPlaying.instance.idLevel);
    }

    public void Retrylevel()
    {
        MenuManager.instance.CloseMenu("PopupDefeat");
        MenuManager.instance.CloseMenu("PopupVictory");
        StartCoroutine(isPlaying.instance.StartImmunity(5f));
        PlayerMovement.instance.transform.position = isPlaying.instance.lastPoint;
        PlayerMovement.instance.setDie(false);
        PlayerMovement.instance.rb.simulated = true;
        isPlaying.instance.addHealth(100);
        HudManager.instance.RecoveryGame();
        isPlaying.instance.time += 30;
        isPlaying.instance.stats = Stats.inGame;
    }

    public void BackToHome()
    {
        MenuManager.instance.CloseMenu("PopupDefeat");
        MenuManager.instance.CloseMenu("PopupVictory");
        isPlaying.instance.stats = Stats.notHere;
        TransitionManager.instance.loadingTransition.startLoading(1f);
        PlayerManager.instance.changePlayer("idle");
        PlayerMovement.instance.setDie(false);
        PlayerMovement.instance.rb.simulated = true;
        SceneManager.LoadScene(1);
        MenuManager.instance.OpenMenu("MainMenu", 10);
    }
}
