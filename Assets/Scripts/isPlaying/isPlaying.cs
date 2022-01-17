using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPlaying : MonoBehaviour
{
    public static isPlaying instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de isPlaying dans la scène");
            return;
        }
        instance = this;
    }


}
