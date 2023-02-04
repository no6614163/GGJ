﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : HappyUtils.SingletonBehaviour<SoundManager>
{

    [SerializeField] AudioSource _bgmSource;
    [SerializeField] AudioSource _sfxSource;

    [SerializeField] SoundList _Bgms;
    [SerializeField] List<SoundList> _Sfxs;

    Dictionary<string, int> toggles = new Dictionary<string, int>();
    public void PlayBGM(string name, float volumeMultiplier = 1f)
    {
        Sound sound = _Bgms.GetSound(name);
        if(sound == null)
        {
            Debug.LogError(name + " 음악을 찾을 수 없음!");
            return;
        }
        _bgmSource.Stop();
        _bgmSource.clip = sound.Clip;
        _bgmSource.volume = sound.Volume * volumeMultiplier;
        _bgmSource.Play();
    }
    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    public void PauseBGM()
    {
        _bgmSource.Pause();
    }
    public void ResumeBGM()
    {
        _bgmSource.Play();
    }
    /// <summary>
    /// Plays sound in the list. If there are multiple sounds with the same name, play one of those randomly.
    /// </summary>
    public void PlaySFX(string soundName, string listTitle, float volumeMultiplier = 1f)
    {
        var list = _Sfxs.Find(x => x.ListTitle == listTitle);
        if (list == null)
        {
            Debug.LogError(listTitle + "에서 " + soundName + " 사운드를 찾을 수 없음!");
            return;
        }
        List<Sound> sounds = list.GetAllSounds(soundName);

        if (sounds.Count == 0)
        {
            Debug.LogError(soundName + " 사운드를 찾을 수 없음!");
            return;
        }
        Sound sound = HappyUtils.Random.RandomElement(sounds);
        _sfxSource.PlayOneShot(sound.Clip, sound.Volume * volumeMultiplier);
    }
    /// <summary>
    /// Plays sound in the list. If there are multiple sounds with the same name, play them in sequence each time.
    /// </summary>
    public void PlaySFXToggle(string soundName, string listTitle, float volumeMultiplier = 1f)
    {
        var list = _Sfxs.Find(x => x.ListTitle == listTitle);
        if (list == null)
        {
            Debug.LogError(listTitle + "에서 " + soundName + " 사운드를 찾을 수 없음!");
            return;
        }
        List<Sound> sounds = list.GetAllSounds(soundName);

        if (sounds.Count == 0)
        {
            Debug.LogError(name + " 사운드를 찾을 수 없음!");
            return;
        }
        int idx = 0;
        if (toggles.ContainsKey(soundName + listTitle))
        {
            idx = toggles[soundName + listTitle];
        }
        else
        {
            toggles[soundName + listTitle] = 0;
        }
        toggles[soundName + listTitle] = (1 + toggles[soundName + listTitle]) % sounds.Count;
        Sound sound = sounds[idx];
        _sfxSource.PlayOneShot(sound.Clip, sound.Volume * volumeMultiplier);
    }
    void PlayBGM(AudioClip bgm, float volume = 0.5f)
    {
        _bgmSource.PlayOneShot(bgm, volume);
    }
    void PlaySFX(AudioClip sfx, float volume = 0.5f)
    {
        _sfxSource.PlayOneShot(sfx, volume);
    }
}
