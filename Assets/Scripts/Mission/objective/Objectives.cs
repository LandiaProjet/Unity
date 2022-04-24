using UnityEngine;

[System.Serializable]
public class Objective
{
    public string description;
    public Sprite sprite;
}

public class Objectives : MonoBehaviour
{
    public Objective[] objectives;
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
