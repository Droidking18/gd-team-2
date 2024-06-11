using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//Class that can not be instantiated
public static class SaveSystem 
{
    // So we can call it from anywhere without an instance
    public static void SavePlayer(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    // Returning data 
    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.data";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Saved file not found in: " + path);
            // Return a default PlayerData object with timer set to 5 minutes and a null reference to Player
            return new PlayerData(null, 300f); 
        }

    }
}
