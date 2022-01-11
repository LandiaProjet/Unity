using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject[] dontDestroyOnLoad;

    public static HudManager hudManager = new HudManager();

    private void Awake() {
        instance = this;
        //Set les valeurs par default
        hudManager.reset();


        DontDestroyOnLoad(gameObject);
        foreach(var obj in dontDestroyOnLoad){
            DontDestroyOnLoad(obj);
        }
    }

    void Update()
    {
        
    }
}
