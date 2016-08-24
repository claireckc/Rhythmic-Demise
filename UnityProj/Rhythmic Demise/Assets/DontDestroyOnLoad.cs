using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad thisMusic = null;

    public static DontDestroyOnLoad Instance
    {
        get { return thisMusic; }
    }

    void Awake()
    {
        if (thisMusic != null && thisMusic != this)
        {
            Destroy(gameObject);
            return;
        }
        else
            thisMusic = this;
        DontDestroyOnLoad(gameObject);
    }
}
