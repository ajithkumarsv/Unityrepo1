using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class AudioManager : Singleton<AudioManager>
    {


        private AudioSource[] audioSources;

        public AudioClip[] fireClips;
        public AudioClip reload;
        public AudioClip bolt;
        public AudioClip[] take;

        private void Awake()
        {

            audioSources = GetComponentsInChildren<AudioSource>();
        }


        public void PlayFire()
        {
            PlaySound(fireClips[Random.Range(0, fireClips.Length)]);
        }
        public void PlayReload()
        {
            Debug.Log("Reload Event called");
            PlaySound(reload);
        }
        public void PlayBolt()
        {
            PlaySound(bolt);
        }
        public void PlayTake()
        {
            PlaySound(take[Random.Range(0, take.Length)]);
        }
        public void PlaySound(AudioClip clip, float volume = 1.0f)
        {
            // Find an available audio source
            AudioSource source = GetAvailableAudioSource();
            if (source == null)
            {
                Debug.LogWarning("No available audio sources!");
                return;
            }

            source.volume = volume;
            source.clip = clip;
            source.Play();
        }

        private AudioSource GetAvailableAudioSource()
        {
            foreach (AudioSource source in audioSources)
            {
                if (!source.isPlaying)
                {
                    return source;
                }
            }
            return null;
        }

        public void StopAllSounds()
        {
            foreach (AudioSource source in audioSources)
            {
                source.Stop();
            }
        }

        public void SetMasterVolume(float volume)
        {
            AudioListener.volume = volume;
        }
    }
}