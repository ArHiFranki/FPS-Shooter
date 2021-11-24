using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private SettingPopup _settingPopup;

    private void Start()
    {
        _settingPopup.Close();
    }

    private void Update()
    {
        _scoreText.text = Time.realtimeSinceStartup.ToString();
    }

    public void OnOpenSetting()
    {
        _settingPopup.Open();
    }

    public void OnPointerDown()
    {
        Debug.Log("Pointer Down");
    }
}
