using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{

    public static PlayerData playerdata;

    public void Awake()
    {
        if (playerdata != null)
            Destroy(gameObject);
        else
        {
            print("Null data");
            DontDestroyOnLoad(gameObject);
            SaveLoadManager.LoadInformation();

            if (playerdata == null)
            {
                print("NUll still");
                playerdata = new PlayerData();
            }
        }

    }
}