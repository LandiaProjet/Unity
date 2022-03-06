using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] dontDestroyOnLoad;
    
    private void Awake() {
        //Limit FPS

        foreach (var element in dontDestroyOnLoad)
        {
            DontDestroyOnLoad(element);
        }
    }
}
