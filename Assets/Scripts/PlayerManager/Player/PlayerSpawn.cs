using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    
    private GameObject respawnPoint;
    private string sceneName;


    public static PlayerSpawn instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de isPlaying dans la scene");
            return;
        }
        instance = this;
    }

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneName != SceneManager.GetActiveScene().name)
        {
            spawnPlayer();
            sceneName = SceneManager.GetActiveScene().name;
        }
    }

    public void spawnPlayer()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnPoint == null)
        {
            if (player.activeSelf == true)
                player.SetActive(false);
        }
        else
        {
            if (player.activeSelf == false)
            {
                player.SetActive(true);
            }
            PlayerMovement.instance.transform.position = new Vector2(respawnPoint.transform.position.x, respawnPoint.transform.position.y);
        }
    }
}
