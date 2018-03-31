using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    public static AudioManager instance = null;
    public AudioSource powerUpCollected;
    public AudioSource powerDownCollected;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
