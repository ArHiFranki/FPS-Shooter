using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private SettingPopup _settingPopup;

    private int _score;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void Start()
    {
        _score = 0;
        _scoreText.text = _score.ToString();

        _settingPopup.Close();
    }

    public void OnOpenSetting()
    {
        _settingPopup.Open();
    }

    public void OnPointerDown()
    {
        Debug.Log("Pointer Down");
    }

    private void OnEnemyHit()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }
}
