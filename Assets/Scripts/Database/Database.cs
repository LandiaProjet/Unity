using System.IO;
using UnityEngine;


public class Database
{
    public string persistentPath;
    public Object data;

    public Database(string nameFile, Object data)
    {
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + nameFile;
        this.data = data;
        LoadData();
    }

    public void SaveData()
    {
        string savePath = persistentPath;

        Debug.Log("Path save : " + savePath);
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public void LoadData()
    {
        try
        {
            using StreamReader reader = new StreamReader(persistentPath);
                
            string json = reader.ReadToEnd();

            JsonUtility.FromJsonOverwrite(json, data);
        }
        catch (FileNotFoundException e)
        {
            Debug.Log(e.ToString());
        }
    }
}
