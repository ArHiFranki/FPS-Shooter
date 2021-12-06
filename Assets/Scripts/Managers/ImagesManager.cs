using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ImagesManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private NetwokrService _network;
    private Texture2D _webImage;

    public void Startup(NetwokrService service)
    {
        Debug.Log("Images manager starting...");

        _network = service;

        status = ManagerStatus.Started;
    }

    public void GetWebImage(Action<Texture2D> callback)
    {
        if (_webImage == null)
        {
            StartCoroutine(_network.DownloadImage(callback));
        }
        else
        {
            callback(_webImage);
        }
    }
}
