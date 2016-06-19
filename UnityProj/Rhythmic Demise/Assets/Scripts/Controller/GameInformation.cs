using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameInformation : MonoBehaviour {
    public const int NUMSTARS = 3;

    public enum UnitType { Null, Cancer, Diebetes };

    public struct SubStage
    {
        float score;
        int stars;
        float[] scoreRange;
    }

    public struct MainStage
    {
        List<SubStage> substages;

    }

    public static UnitType unitType { get; set; }
    public static MainStage gameProgress { get; set; }


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    
    
}
