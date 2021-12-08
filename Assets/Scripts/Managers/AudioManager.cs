using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource _audioSource;

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
}
