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

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerManager dans la scène");
            return;
        }
        instance = this;
    }

    private void Start() {
        mode = "idle";
    }

    private void OnEnable() {
        MenuManager.instance.OpenMenu("HUD");
    }

    private void OnDisable() {
        MenuManager.instance.CloseMenu("HUD");
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            changePlayer("bow");
        if (Input.GetKeyDown(KeyCode.V))
            changePlayer("sword");
        if (Input.GetKeyDown(KeyCode.B))
            changePlayer("idle");
    }*/

    public void SwitchModePlayer()
    {
        if (isPlaying.instance.stats != Stats.inGame)
            return;
        switch (mode)
        {
            case "sword":
                if (isPlaying.instance.GetCount(0) > 0)
                    changePlayer("bow");
                else
                    changePlayer("idle");
                break;
            case "bow":
                changePlayer("idle");
                break;
            case "idle":
                if (isPlaying.instance.GetCount(1) > 0)
                    changePlayer("sword");
                else
                    if (isPlaying.instance.GetCount(0) > 0)
                        changePlayer("bow");
                    else
                        changePlayer("idle");
                break;
        }
    }

    public void changePlayer(string mode){
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
        ButtonManager.instance.checkButtonType();
    }
}