using UnityEngine;


namespace Database{

    public class test : MonoBehaviour
    {
        public static Database database = new Database();

        private void Awake() {
            float test = database.gameData.health;
        }
    }
}