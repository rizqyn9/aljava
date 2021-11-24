using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Aljava
{
    public class SoundManager : Singleton<SoundManager>
    {
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
    }
}
