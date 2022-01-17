using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public LoadingTransition loadingTransition;
    public FadeTransition fadeTransition;

    public static TransitionManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de TransitionManager dans la scène");
            return;
        }
        instance = this;
    }
}
