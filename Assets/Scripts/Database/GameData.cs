using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database{

    public class GameData
    {
        public float health;
        public int level;

        public GameData(float health, int level)
        {
            this.health = health;
            this.level = level;
        }

        public override string ToString()
        {
            return "health:" + health + " | level:" + level;
        }
    }
}