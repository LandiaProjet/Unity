using System.IO;
using UnityEngine;

namespace Database{
    public class Database : MonoBehaviour
    {
        public GameData gameData;

        private string path = "";
        private string persistentPath = "";

        // Start is called before the first frame update
        void Start()
        {
            CreateGameData();
            SetPaths();
        }

        private void CreateGameData()
        {
            gameData = new GameData(100f, 3,0);
        }

        private void SetPaths()
        {
            path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
            persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                SaveData();

            if (Input.GetKeyDown(KeyCode.L))
                LoadData();
        }

        public void SaveData()
        {
            string savePath = persistentPath;

            Debug.Log("Path save : " + savePath);
            string json = JsonUtility.ToJson(gameData);
            Debug.Log(json);

            using StreamWriter writer = new StreamWriter(savePath);
            writer.Write(json);
        }

        public void LoadData()
        {
            using StreamReader reader = new StreamReader(persistentPath);
            string json = reader.ReadToEnd();

            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log(data.ToString());
        }
    }
}