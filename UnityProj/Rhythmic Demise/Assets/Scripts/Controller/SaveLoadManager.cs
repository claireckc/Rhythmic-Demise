using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveLoadManager
{
    public static void SaveAllInformation()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        bf.Serialize(stream, GameInformation.gameInfo);
        stream.Close();
    }

    public static void LoadInformation()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            GameInformation.gameInfo = bf.Deserialize(stream) as GameInformation;

            stream.Close();
        }
    }
}
