using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private Material _skyMaterial;
    [SerializeField] private Light _sunLight;

    private float _fullIntensity;
    private float _cloudValue = 0f;
    private float _cluudValueMax = 1f;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.WEATHER_UPDATE, OnWeatherUpdated);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WEATHER_UPDATE, OnWeatherUpdated);
    }

    private void Start()
    {
        _fullIntensity = _sunLight.intensity;
    }

    private void SetOvercast(float value)
    {
        _skyMaterial.SetFloat("_Blend", value);
        _sunLight.intensity = _fullIntensity - (_fullIntensity * value);
    }

    private void OnWeatherUpdated()
    {
        if (_cloudValue < _cluudValueMax)
        {
            SetOvercast(Managers.Weather.cloudValue);
        }
    }
}
