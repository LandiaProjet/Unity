using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Leguar.TotalJSON;

public class Database : MonoBehaviour
{
    private string FILE_PATH = Application.persistentDataPath + "/data.json";

    JSON jsonObject = new JSON();

    public JValue Get(string key)
    {
        return jsonObject.Get(key);
    }

    public void Set(string key, Object value)
    {
        jsonObject.Add(key, value);
    }

    private void Awake() {
    }

    private void saveJsonObjectToTextFile() {
        string jsonAsString = jsonObject.CreateString(); // Could also use "CreatePrettyString()" to make more human readable result, it is still valid JSON to read and parse by computer
        StreamWriter writer = new StreamWriter(FILE_PATH);
        writer.WriteLine(jsonAsString);
        writer.Close();
    }
}
