using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] footstep_sounds;
    [SerializeField] AudioClip thud_sound;
    private AudioSource audio_source;

    private bool sound_enabled;

    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
        GameData.footstep_sound_enabled = true;
    }

    public void PlayFootstepSound()
    {
        if (GameData.footstep_sound_enabled)
        {
            audio_source.PlayOneShot(GetRandomFootstepClip());
        }   
    }

    public void PlayThudSound()
    {
        audio_source.PlayOneShot(thud_sound);
    }

    public void PlayTrickFailSound()
    {
        audio_source.PlayOneShot(GetRandomFootstepClip());
    }

    public void EnableFootstepSounds(bool value)
    {
        GameData.footstep_sound_enabled = value;
    }

    private AudioClip GetRandomFootstepClip()
    {
        return footstep_sounds[Random.Range(0, footstep_sounds.Length)];
    }
}
