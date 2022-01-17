using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    public Objectives[] objectives;
    public static Objectives instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Objectives dans la scène");
            return;
        }
        instance = this;
    }
}
