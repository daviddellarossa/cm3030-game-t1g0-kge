using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    private bool isMuted;

    void Start()
    {
        isMuted = false;
    }

    public void MutedPressed()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue)*20);
    }
}
