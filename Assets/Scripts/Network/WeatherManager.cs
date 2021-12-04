using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private NetwokrService _network;

    public void Startup(NetwokrService service)
    {
        Debug.Log("Weather manager starting...");

        _network = service;

        status = ManagerStatus.Started;
    }
}
