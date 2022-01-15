using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database{

    public class GameData
    {
        public float health;
        public int level;
        public float xp;

        public GameData(float health, int level, float xp)
        {
            this.health = health;
            this.level = level;
            this.xp = xp;
        }

        public override string ToString()
        {
            return "health:" + health + " | level:" + level;
        }
    }
}