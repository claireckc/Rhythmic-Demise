using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameInformation : MonoBehaviour {
    static GameInformation gameFab;

    struct SubStageStruct
    {
        int numberOfStage;

    }


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        if (gameFab != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            gameFab = this;
        }
    }
    
    
}
