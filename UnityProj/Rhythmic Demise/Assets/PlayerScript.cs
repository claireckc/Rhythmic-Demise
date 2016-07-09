using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

    public static PlayerScript playerScript;
    
    public void Awake()
    {
        if (playerScript != null)
            Destroy(gameObject);
        else
        {
            print("Null data");
            DontDestroyOnLoad(gameObject);
            SaveLoadManager.LoadInformation();
            if (playerScript == null) {

                print("NUll still");
                playerScript = new PlayerScript();
            }
        }

    }
}
