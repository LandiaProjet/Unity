using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float health;
    public float money;

    public int level;
    public float experience;
    public float experienceToNextLevel;

    private Database database;

    private void Start()
    {
        database = new Database("Player.json", this);
    }
}