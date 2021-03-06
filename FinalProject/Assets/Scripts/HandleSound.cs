using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSound : MonoBehaviour
{
    public AudioSource[] sounds;
    public AudioSource[] music;
    public AudioSource ambience;
    
    public void PlaySound(int soundIndex)
    {
        if (!sounds[soundIndex].isPlaying) sounds[soundIndex].Play();
    }

    public void StopSound(int soundIndex)
    {
        if (sounds[soundIndex].isPlaying) sounds[soundIndex].Stop();
    }

    public void StartMusic()
    {
        foreach (AudioSource source in music)
        {
            source.volume = 0;
            source.Play();
        }
        StartCoroutine("FadeIn", 0);
    }

    public void StopMusic()
    {
        StartCoroutine("MusicOut");
    }

    public IEnumerator FadeIn(int musicIndex)
    {
        while (music[musicIndex].volume < 1)
        {
            music[musicIndex].volume += Time.deltaTime;
            music[musicIndex].volume = Mathf.Clamp(music[musicIndex].volume, 0f, 1f);
            yield return null;
        }
    }

    public IEnumerator MusicOut()
    {
        while (ambience.volume > 0)
        {
            foreach(AudioSource source in music)
            {
                source.volume -= 0.5f * Time.deltaTime;
                source.volume = Mathf.Clamp(source.volume, 0f, 1f);
            }
            ambience.volume -= 0.5f * Time.deltaTime;
            ambience.volume = Mathf.Clamp(ambience.volume, 0f, 1f);
            yield return null;
        }
    }
}
