using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] dontDestroyOnLoad;
    
    public static GameManager instance;

    public LevelSystem levelSystem = new LevelSystem();

    private void Awake() {
        instance = this;

        foreach (var element in dontDestroyOnLoad)
        {
            DontDestroyOnLoad(element);
        }
    }

    public LevelSystem GetLevelSystem(){
        return levelSystem;
    }
}
