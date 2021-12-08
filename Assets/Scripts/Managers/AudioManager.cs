using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _music1Source;

    [SerializeField] private string _introBGMusic;
    [SerializeField] private string _levelBGMusic;

    public ManagerStatus status { get; private set; }
    public float soundVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }
    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    private NetwokrService _network;

    public void Startup(NetwokrService service)
    {
        Debug.Log("Audio manager starting...");
        _network = service;

        soundVolume = 1f;

        status = ManagerStatus.Started;
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void PlayIntroMusic()
    {
        PlayMusic(Resources.Load("Music/" + _introBGMusic) as AudioClip);
    }

    public void PlayLevelMusic()
    {
        PlayMusic(Resources.Load("Music/" + _levelBGMusic) as AudioClip);
    }

    private void PlayMusic(AudioClip clip)
    {
        _music1Source.clip = clip;
        _music1Source.Play();
    }

    public void StopMusic()
    {
        _music1Source.Stop();
    }
}
