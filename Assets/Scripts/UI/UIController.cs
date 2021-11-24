using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private void Update()
    {
        _scoreText.text = Time.realtimeSinceStartup.ToString();
    }

    public void OnOpenSetting()
    {
        Debug.Log("Open Setting");
    }

    public void OnPointerDown()
    {
        Debug.Log("Pointer Down");
    }
}
