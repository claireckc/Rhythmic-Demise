using UnityEngine;
using System.Collections;

public class SaveInformation : MonoBehaviour {

    static SaveInformation instance;

    public static void SaveAllInformation()
    {
        //PlayerPrefs.SetInt
    }

	// Use this for initialization
	void Start () {

        if (instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
