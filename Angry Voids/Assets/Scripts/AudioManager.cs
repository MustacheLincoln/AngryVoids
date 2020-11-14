using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static GameObject bgAudio;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (bgAudio)
        { 
            Destroy(gameObject);
        }
        else
        { 
            bgAudio = gameObject;
        }
    }
}