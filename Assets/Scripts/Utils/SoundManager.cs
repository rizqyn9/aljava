using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Aljava
{
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioMixer mixer;

        public AudioClip BGM;
        public AudioClip ButtonSFX;
        public AudioClip Star1;
        public AudioClip Star2;
        public AudioClip Star3;
        public AudioClip Win;
        public AudioClip Lose;
        public AudioClip RepairSFX;
        public AudioClip MachineDoneSFX;
        public AudioClip MachineSoundSFX;
        public AudioClip MachineOverheatSFX;
        public AudioSource BGM1;


        public AudioSource soundFX, soundMusic;

        public static void PlaySound(AudioClip _audioClip)
        {
            Instance.soundFX.PlayOneShot(_audioClip);
        }

        public static void PlayButtonSFX() => Instance.soundFX.PlayOneShot(Instance.ButtonSFX);

        public enum AudioTarget
        {
            BGM,
            SFX
        }

        public void changeVolume(float _value, AudioTarget _target)
        {
            if (_target == AudioTarget.BGM) mixer.SetFloat("MusicVol", Mathf.Log10(_value) * 20);
            if (_target == AudioTarget.SFX) mixer.SetFloat("SFXVol", Mathf.Log10(_value) * 20);
        }

    }
}
