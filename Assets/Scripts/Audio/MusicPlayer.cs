using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] music_clips;
    private AudioSource audio_source;

    private void Start()
    {
        LoadComponents();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseMusic();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResumeMusic();
        }
    }

    private void LoadComponents()
    {
        audio_source = GetComponent<AudioSource>();
    }

    public void StartMusic()
    {
        audio_source.clip = music_clips[0];
        audio_source.Play();
    }

    public void PauseMusic()
    {
        audio_source.Pause();
    }

    public void ResumeMusic()
    {
        audio_source.UnPause();
    }
}
