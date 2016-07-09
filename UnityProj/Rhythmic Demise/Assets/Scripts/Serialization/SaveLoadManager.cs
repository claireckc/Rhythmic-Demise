using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveLoadManager{

    public static BinaryFormatter bf = new BinaryFormatter();
    
    public static void SaveAllInformation(PlayerData pd)
    {
        PlayerData data = new PlayerData();
        data = pd;
        Debug.Log("dont exist: " + data.pathogenType);
        Debug.Log("1 save");
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);
        Debug.Log("2 save");

        bf.Serialize(stream, data);
        Debug.Log("3 save");
        stream.Close();
    }

    public static void LoadInformation()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            Debug.Log("exist: " + Application.persistentDataPath + "/player.sav");
            Debug.Log("1 load");
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);
            Debug.Log("2 load");

            PlayerScript.playerdata = bf.Deserialize(stream) as PlayerData;
            Debug.Log("3 load");

            stream.Close();
            Debug.Log("Finish loading: " + PlayerScript.playerdata.pathogenType);
            
        }
    }

    public static void EraseInformation()
    {
        if(File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            Debug.Log("File exists!");
            File.Delete(Application.persistentDataPath + "/player.sav");
            Debug.Log(File.Exists(Application.persistentDataPath + "/player.sav"));
        }

    }
    
}

