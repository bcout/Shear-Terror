using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] music_clips;
    private AudioSource audio_source;
    private int music_clip_index;

    private bool music_paused;

    private void Awake()
    {
        LoadLocalComponents();
    }

    private void Start()
    {
        music_paused = false;
        music_clip_index = 0;
    }

    private void Update()
    {
        if (GameData.game_paused && !music_paused)
        {
            PauseMusic();
        }
        else if (!GameData.game_paused && music_paused && !GameData.currently_going_to_main_menu)
        {
            ResumeMusic();
        }
    }

    private void LoadLocalComponents()
    {
        audio_source = GetComponent<AudioSource>();
    }

    public void StartMusic()
    {
        audio_source.clip = music_clips[0];
        audio_source.Play();
    }

    public void StopMusic()
    {
        audio_source.Stop();
    }

    public void PauseMusic()
    {
        audio_source.Pause();
        music_paused = true;
    }

    public void ResumeMusic()
    {
        audio_source.UnPause();
        music_paused = false;
    }

    public void IncreaseTempo()
    {
        music_clip_index++;
        audio_source.clip = music_clips[music_clip_index % 5];
        audio_source.Play();
    }
}
