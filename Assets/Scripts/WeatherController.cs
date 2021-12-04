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

    private void Start()
    {
        _fullIntensity = _sunLight.intensity;
    }

    private void Update()
    {
        SetOvercast(_cloudValue);

        if (_cloudValue < _cluudValueMax)
        {
            _cloudValue += 0.005f;
        }
    }

    private void SetOvercast(float value)
    {
        _skyMaterial.SetFloat("_Blend", value);
        _sunLight.intensity = _fullIntensity - (_fullIntensity * value);
    }
}
