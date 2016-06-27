using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveLoadManager{

    /*public static SaveLoadManager SLMng;

    public void Awake()
    {
        System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");

        SLMng = this;
        if (SLMng != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            SLMng = this;
        }
    }*/
    
    public static void SaveAllInformation()
    {
        Debug.Log("dont exist");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);
        Debug.Log(stream);
        GameInformation gi = GameInformation.gameInfo;

        bf.Serialize(stream, gi);
        stream.Close();
    }

    public static GameInformation LoadInformation()
    {
        GameInformation gi = GameInformation.gameInfo;
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            Debug.Log("exist: " + Application.persistentDataPath + "/player.sav");
            BinaryFormatter bf = new BinaryFormatter();
            Debug.Log("1");
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);
            Debug.Log("2");

            gi = bf.Deserialize(stream) as GameInformation;
            Debug.Log("3");

            stream.Close();
            Debug.Log("Finish loading");
            
        }

        return gi;
    }
    
}
