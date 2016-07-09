using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveLoadManager{
    
    public static void SaveAllInformation(PlayerData pd)
    {
        Debug.Log("dont exist");
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log("1");
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);
        Debug.Log("2");

        bf.Serialize(stream, pd);
        Debug.Log("3");
        stream.Close();
    }

    public static PlayerData LoadInformation()
    {
        PlayerData pd = null;
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            Debug.Log("exist: " + Application.persistentDataPath + "/player.sav");
            BinaryFormatter bf = new BinaryFormatter();
            Debug.Log("1");
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);
            Debug.Log("2");

            pd = bf.Deserialize(stream) as PlayerData;
            Debug.Log("3");

            stream.Close();
            Debug.Log("Finish loading");
            
        }

        return pd;
    }
    
}
