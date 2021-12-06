using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using MiniJSON;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public float cloudValue { get; private set; }

    private NetwokrService _network;

    public void Startup(NetwokrService service)
    {
        Debug.Log("Weather manager starting...");

        _network = service;
        //StartCoroutine(_network.GetWeatherXML(OnXMLDataLoaded));
        StartCoroutine(_network.GetWeatherJSON(OnJSONDataLoaded));

        status = ManagerStatus.Initializing;
    }

    public void OnXMLDataLoaded(string data)
    {
        XmlDocument document = new XmlDocument();
        document.LoadXml(data);
        XmlNode root = document.DocumentElement;

        XmlNode node = root.SelectSingleNode("clouds");
        string value = node.Attributes["value"].Value;
        cloudValue = Convert.ToInt32(value) / 100f;
        Debug.Log("Value: " + cloudValue);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATE);

        status = ManagerStatus.Started;
    }

    public void OnJSONDataLoaded(string data)
    {
        Dictionary<string, object> dictionary;
        dictionary = Json.Deserialize(data) as Dictionary<string, object>;

        Dictionary<string, object> clouds = (Dictionary<string, object>)dictionary["clouds"];
        cloudValue = (long)clouds["all"] / 100f;
        Debug.Log("Value: " + cloudValue);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATE);

        status = ManagerStatus.Started;
    }
}
