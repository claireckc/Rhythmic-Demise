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
            DontDestroyOnLoad(gameObject);
            SaveLoadManager.LoadInformation();

            if (playerdata == null)
                playerdata = new PlayerData();
        }
    }
}