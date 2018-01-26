using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;

    public void StopAudio()
    {
        source.Stop();
    }

    public void PlayAudio()
    {
        source.Play();
    }
}