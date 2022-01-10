using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject[] dontDestroyOnLoad;

    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
        foreach(var obj in dontDestroyOnLoad){
            DontDestroyOnLoad(obj);
        }
    }

    void Update()
    {
        
    }
}
