using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<Sound> sounds;
    public AudioSource background;
    public AudioSource SFX;

    // private void Start() 
    // {
    //     PlaySoundBackGround(SoundName.Background1);
    // }

    // public void PlaySoundBackGround(SoundName soundName)
    // {
    //     foreach (var sound in sounds)
    //     {
    //         if (sound.soundName == soundName)
    //         {
    //             background.clip = sound.audioClip;
    //             background.Play();
    //         }
    //     }
    // }

    public void PlaySFX(SoundName soundName)
    {
        foreach (var sound in sounds)
        {
            if (sound.soundName == soundName)
            {
                SFX.clip = sound.audioClip;
                SFX.Play();
            }
        }
    }

}

[Serializable]
public class Sound
{
    public SoundName soundName;
    public AudioClip audioClip;
}

public enum SoundName
{
    Shoot,
    Background1,
    Background2,
    EnemyShoot,
    CharacterEnd
}