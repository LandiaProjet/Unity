using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    public int level;
    public int health;
    public int money;
    public int xp;

    public PlayerData(Player player)
    {
        level = player.level;
        health = player.health;
        money = player.money;
        xp = player.xp;
    }
}
