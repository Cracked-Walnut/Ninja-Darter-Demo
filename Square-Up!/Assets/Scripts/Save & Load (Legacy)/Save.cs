using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Save {

    // private string path = Application.persistentDataPath + "saveData.bf";

    public static void save(Player player) {
        
        string path = Application.persistentDataPath + "saveData.bf";
        
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData loadPlayer() {

        string path = Application.persistentDataPath + "saveData.bf";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;

        } else {
            Debug.LogError ("Save file not found: " + path);
            return null;
        }
    }

}

/*
Sources:
1) B., Brackeys, 'SAVE & LOAD SYSTEM in Unity', 2018. [Online]. Available: https://www.youtube.com/watch?v=XOjd_qU2Ido [Accessed: 02-Mar-2020].
*/