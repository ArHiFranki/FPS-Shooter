using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetwokrService
{
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
        return CallAPI(APIKey.WeatherAPIKey, callback);
    }
}
