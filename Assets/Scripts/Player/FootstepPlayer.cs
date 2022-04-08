using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] footstep_sounds;
    private AudioSource audio_source;

    private bool sound_enabled;

    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
        sound_enabled = true;
    }

    private void PlayFootstepSound()
    {
        if (sound_enabled)
        {
            audio_source.PlayOneShot(GetRandomClip());
        }   
    }

    public void EnableFootstepSounds(bool value)
    {
        sound_enabled = value;
    }

    private AudioClip GetRandomClip()
    {
        return footstep_sounds[Random.Range(0, footstep_sounds.Length)];
    }
}
