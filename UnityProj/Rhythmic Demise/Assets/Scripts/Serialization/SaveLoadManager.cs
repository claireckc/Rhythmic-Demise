using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveLoadManager
{

    public static BinaryFormatter bf = new BinaryFormatter();

    public static void SaveAllInformation(PlayerData pd)
    {
        PlayerData data = new PlayerData();
        data = pd;
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);
        Debug.Log(Application.persistentDataPath);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static void LoadInformation()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            Debug.Log(Application.persistentDataPath);
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            PlayerScript.playerdata = bf.Deserialize(stream) as PlayerData;

            stream.Close();

        }
    }

    public static void EraseInformation()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            File.Delete(Application.persistentDataPath + "/player.sav");
        }

    }

}
