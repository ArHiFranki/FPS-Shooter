using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _music1Source;
    [SerializeField] private AudioSource _music2Source;

    [SerializeField] private string _introBGMusic;
    [SerializeField] private string _levelBGMusic;

    [SerializeField] private float _crossFadeRate = 1.5f;

    public ManagerStatus status { get; private set; }
    public float soundVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }
    public float musicVolume
    {
        get { return _musicVolume; }
        set 
        {
            _musicVolume = value;
            if (_music1Source != null && !_crossFading)
            {
                _music1Source.volume = _musicVolume;
                _music2Source.volume = _musicVolume;
            }
        }
    }
    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }
    public bool musicMute
    {
        get 
        {
            if (_music1Source != null)
            {
                return _music1Source.mute;
            }
            return false;
        }
        set
        {
            if (_music1Source != null)
            {
                _music1Source.mute = value;
                _music2Source.mute = value;
            }
        }
    }

    private NetwokrService _network;
    private AudioSource _activeMusic;
    private AudioSource _inactiveMusic;
    private float _musicVolume;
    private bool _crossFading;

    public void Startup(NetwokrService service)
    {
        Debug.Log("Audio manager starting...");
        _network = service;

        _music1Source.ignoreListenerVolume = true;
        _music2Source.ignoreListenerVolume = true;
        _music1Source.ignoreListenerPause = true;
        _music2Source.ignoreListenerPause = true;

        soundVolume = 1f;
        musicVolume = 1f;

        _activeMusic = _music1Source;
        _inactiveMusic = _music2Source;

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
        if (_crossFading) { return; }
        StartCoroutine(CrossFadeMusic(clip));
    }

    public void StopMusic()
    {
        _activeMusic.Stop();
        _inactiveMusic.Stop();
    }

    private IEnumerator CrossFadeMusic(AudioClip clip)
    {
        _crossFading = true;

        _inactiveMusic.clip = clip;
        _inactiveMusic.volume = 0;
        _inactiveMusic.Play();

        float scaleRate = _crossFadeRate * _musicVolume;
        while (_activeMusic.volume > 0)
        {
            _activeMusic.volume -= scaleRate * Time.deltaTime;
            _inactiveMusic.volume += scaleRate * Time.deltaTime;

            yield return null;
        }

        AudioSource temp = _activeMusic;

        _activeMusic = _inactiveMusic;
        _activeMusic.volume = _musicVolume;

        _inactiveMusic = temp;
        _inactiveMusic.Stop();
        _crossFading = false;
    }
}
