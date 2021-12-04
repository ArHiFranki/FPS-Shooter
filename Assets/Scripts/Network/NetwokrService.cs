using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetwokrService
{
    private const string xmlApi = 
    "http://api.openweathermap.org/data/2.5/weather?q=Novosibirsk,ru&mode=xml&APPID=e1e8d773b4bbc8614c2bcf707372a4e5";

    private IEnumerator CallAPI(string url, Action<string> callback )
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.Send();

            if (request.isNetworkError)
            {
                Debug.LogError("Network problem: " + request.error);
            }
            else if (request.responseCode != (long)System.Net.HttpStatusCode.OK)
            {
                Debug.LogError("Response error: " + request.responseCode);
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, callback);
    }
}
