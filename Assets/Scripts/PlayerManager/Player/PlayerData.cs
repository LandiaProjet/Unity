using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public float health;
    public int lastTime;
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
    }

    public static PlayerData getData()
    {
        return instance;
    }

    private void FixedUpdate()
    {
        int timeNow = calculateSeconds();
        int result = timeNow - lastTime;
        
        if (result > 1800)
        {
            int countHealth = result / 1800;

            health += countHealth;
            if (health > 5)
                health = 5;
            lastTime = timeNow;
            database.SaveData();
        }
    }

    int calculateSeconds()
    {

        DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

        DateTime dtNow = DateTime.Now;

        TimeSpan result = dtNow.Subtract(dt);

        int seconds = Convert.ToInt32(result.TotalSeconds);

        return seconds;
    }

    DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }

    public void AddHealth()
    {
        health++;
        database.SaveData();
    }

    public void RemoveHealth()
    {
        health--;
        database.SaveData();
    }

    public void AddCredit(int credit)
    {
        money += credit;
        database.SaveData();
    }

    public void RemoveCredit(int credit)
    {
        money -= credit;
        database.SaveData();
    }

    public string getTimeHealth()
    {
        if (health == 5)
        {
            return "full";
        }
        else
        {
            long timeBeforeHealth = UnixTimeStampToDateTime(lastTime).AddMinutes(30).Ticks - DateTime.Now.Ticks;
            TimeSpan ts = new TimeSpan(timeBeforeHealth);

            return string.Format("{0:00}:{1:00}:{2:00}", ts.Days, ts.Minutes, ts.Seconds);
        }
    }
}