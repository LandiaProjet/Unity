using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    public RuntimeAnimatorController runtimeAnimatorControllerIdle;
    public RuntimeAnimatorController runtimeAnimatorControllerSword;
    public RuntimeAnimatorController runtimeAnimatorControllerBow;

    public string mode;

    public LevelSystem levelSystem = new LevelSystem();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerManager dans la scène");
            return;
        }
        instance = this;

        levelSystem.OnLevelChanged += this.OnPlayerLevelChanged;
        levelSystem.OnExperienceChanged += this.OnPlayerExperienceChanged;
    }

    private void Start() {
        mode = "idle";

        //test Level
        levelSystem.AddExperience(100);
        Debug.Log("test-"+levelSystem.GetExperience());
        PlayerData.getData().database.SaveData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            changePlayer("bow");
        if (Input.GetKeyDown(KeyCode.V))
            changePlayer("sword");
        if (Input.GetKeyDown(KeyCode.B))
            changePlayer("idle");
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

    private void OnPlayerLevelChanged(object sender, System.EventArgs e){
        //PlayerData.getData().level = ((LevelSystem)sender).GetLevelNumber();
    }

    private void OnPlayerExperienceChanged(object sender, System.EventArgs e){
      //  PlayerData.getData().experience = ((LevelSystem)sender).GetExperience();
    }

}