using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic backgroundMusic;
    void Awake()
    {
        if(backgroundMusic != null)
        {
            Destroy(gameObject);
        } 
        else
        {
            backgroundMusic = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
