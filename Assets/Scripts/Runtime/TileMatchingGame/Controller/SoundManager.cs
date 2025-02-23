using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller
{
    public class SoundManager : ISoundManager
    {
        private AudioSource _musicSource;
        private AudioSource _sfxSource;
        private Dictionary<string, AudioClip> _soundLibrary = new Dictionary<string, AudioClip>();

        public SoundManager(AudioSource musicSource, AudioSource sfxSource)
        {
            _musicSource = musicSource;
            _sfxSource = sfxSource;
            LoadSounds();
        }

        private void LoadSounds()
        {
            AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio/");
            foreach (var clip in clips)
            {
                _soundLibrary[clip.name] = clip;
            }
        }

        public void PlaySound(string soundName)
        {
            if (_soundLibrary.ContainsKey(soundName))
            {
                _sfxSource.PlayOneShot(_soundLibrary[soundName]);
            }
        }

        public void PlayMusic(string musicName, bool loop = true)
        {
            if (_soundLibrary.ContainsKey(musicName))
            {
                _musicSource.clip = _soundLibrary[musicName];
                _musicSource.loop = loop;
                _musicSource.Play();
            }
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }

        public void SetSFXVolume(float volume)
        {
            _sfxSource.volume = volume;
        }

        public void SetMusicVolume(float volume)
        {
            _musicSource.volume = volume;
        }
    }

}