using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public float health;
    public long tickForNextHealth;
    public float money;

    public int level;
    public int experience;

    public Database database;
    private static PlayerData instance;

    private DateTime NextDateForHealth;

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
        NextDateForHealth = new DateTime(tickForNextHealth);
    }

    public static PlayerData getData()
    {
        return instance;
    }

    private void FixedUpdate()
    {
        if (health < 5 && NextDateForHealth.Ticks <= DateTime.Now.Ticks)
        {
            health++;
            if (health < 5)
            {
                NextDateForHealth = new DateTime(DateTime.Now.AddMinutes(30).Ticks);
                tickForNextHealth = NextDateForHealth.Ticks;
            }
            Debug.Log("changement");
            database.SaveData();
        }
    }

    public string getTimeHealth()
    {
        if (health == 5)
        {
            return "full";
        }
        else
        {
            long timeBeforeHealth = NextDateForHealth.Ticks - DateTime.Now.Ticks;
            TimeSpan ts = new TimeSpan(timeBeforeHealth);

            return string.Format("{0:00}:{1:00}:{2:00}", ts.Days, ts.Minutes, ts.Seconds);
        }
    }
}