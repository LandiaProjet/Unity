using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    public RuntimeAnimatorController runtimeAnimatorControllerIdle;
    public RuntimeAnimatorController runtimeAnimatorControllerSword;
    public RuntimeAnimatorController runtimeAnimatorControllerBow;

    public string mode;

    private GameObject respawnPoint;

    private Database database;
    public PlayerData playerData;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerManager dans la scène");
            return;
        }
  
        instance = this;

        playerData = new PlayerData();
    }

    private void Start() {
        database = new Database("Player.json", playerData);
        
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        mode = "idle";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Respawn();
        if (Input.GetKeyDown(KeyCode.C))
            changePlayer("bow");
        if (Input.GetKeyDown(KeyCode.V))
            changePlayer("sword");
        if (Input.GetKeyDown(KeyCode.B))
            changePlayer("idle");
    }

    void Respawn(){
        if(respawnPoint != null){
            PlayerMovement.instance.transform.position = new Vector2(respawnPoint.transform.position.x, respawnPoint.transform.position.y);
        }
    }

    void changePlayer(string mode){
        switch(mode){
            case "sword":
                this.mode = mode;
                PlayerMovement.instance.animator.runtimeAnimatorController = runtimeAnimatorControllerSword;
                break;
            case "bow":
                this.mode = mode;
                PlayerMovement.instance.animator.runtimeAnimatorController = runtimeAnimatorControllerBow;
                break;
            default:
                this.mode = "idle";
                PlayerMovement.instance.animator.runtimeAnimatorController = runtimeAnimatorControllerIdle;
                break;
        }
    }

    
}