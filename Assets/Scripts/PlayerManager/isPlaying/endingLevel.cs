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
        /*if (PlayerData.getData().health <= 0)
        {
            Popup.instance.openPopup("Alerte", "Vous n'avez pas assez de coeur pour pouvoir relancer une parti ...", 20);
            return;
        }*/
        MenuManager.instance.CloseMenu("PopupDefeat");
        MenuManager.instance.CloseMenu("PopupVictory");
        PlayerManager.instance.changePlayer("idle");
        PlayerMovement.instance.setDie(false);
        PlayerData.getData().RemoveHealth();
        PlayerMovement.instance.rb.simulated = true;
        InterstitialAds.interstitialAds.OnEnd.AddListener(() =>
        {
            LevelManager.instance.openLevel(isPlaying.instance.idLevel);
            StartCoroutine(WaitBeforeRespawnPlayer());
        });
        InterstitialAds.interstitialAds.ShowAd();
    }

    IEnumerator WaitBeforeRespawnPlayer()
    {
        PlayerSpawn.instance.spawnPlayer();
        yield return new WaitForSeconds(0.5f);
        PlayerMovement.instance.rb.AddForce(new Vector2(-10, 2));
    }

    Vector3 GetClosestPoint(Collider2D[] results, Vector3 currentPosition)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (Collider2D potentialTarget in results)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        if (bestTarget == null)
            return currentPosition;
        return bestTarget.position;
    }

    public void Retrylevel()
    {
        MenuManager.instance.CloseMenu("PopupDefeat");
        MenuManager.instance.CloseMenu("PopupVictory");
        MenuManager.instance.CloseMenu("PopupRetry");
        StartCoroutine(isPlaying.instance.StartImmunity(5f));
        if (!PlayerMovement.instance.isGrounded)
        {
            Collider2D[] results = Physics2D.OverlapCircleAll(PlayerMovement.instance.groundCheck.position, 25f, LayerMask.GetMask("RespawnPoint"));
            PlayerMovement.instance.transform.position = GetClosestPoint(results, PlayerMovement.instance.groundCheck.position);
            PlayerMovement.instance.transform.position += new Vector3(0, 0, 105);
        }
        PlayerMovement.instance.setDie(false);
        PlayerMovement.instance.rb.simulated = true;
        isPlaying.instance.stats = Stats.inGame;
        isPlaying.instance.shield = 100;
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
