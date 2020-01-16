using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource bgMusic;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        bgMusic = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (bgMusic.isPlaying) return;
        bgMusic.Play();
    }

    public void StopMusic()
    {
        bgMusic.Stop();
    }
}
