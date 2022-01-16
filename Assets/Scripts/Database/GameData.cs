using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database{

    public class GameData : MonoBehaviour
    {
        public float health;
        public int level;

        private Database database;

        private void Start()
        {
            database = new Database("SaveData.json", this);
        }
    }
}