using UnityEngine;


namespace Database{

    public class test : MonoBehaviour
    {
        public static Database database = new Database();

        private void Awake() {
            database.gameData.health = 10;
            database.SaveData();
        }
    }
}