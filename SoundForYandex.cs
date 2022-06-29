using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Match3 {
    public class SoundForYandex : MonoBehaviour
    {
        AudioSource sound;
        bool pause = false;

        public void init(Ads yandex)
        {
            sound = GetComponent<AudioSource>();
            yandex.SDK.OpenDel += SoundOff;
            yandex.SDK.CloseOrErrorDel += SoundOn;
            yandex.SDK.SoundInit();

        }
        public void SoundOn()
        {
            if (pause) { sound.Play(); pause = false; }
            print("звук включен");
        }
        public void SoundOff()
        {
            if (sound.isPlaying) sound.Pause();
            pause = true;
            print("звук выключен");
        }
        
    } }
