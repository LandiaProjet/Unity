using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float health;
    public float money;

    public int level;
    public int experience;

    public Database database;
    private static PlayerData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerData dans la sc�ne");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        database = new Database("Player.json", this);
    }

    public static PlayerData getData()
    {
        return instance;
    }
}