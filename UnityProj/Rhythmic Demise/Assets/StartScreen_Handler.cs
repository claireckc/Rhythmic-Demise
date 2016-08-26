using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class StartScreen_Handler : MonoBehaviour {
    
    public void StartPress()
    {
        Application.LoadLevel("StartScreen");
    }
}
