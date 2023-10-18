using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _resumeButton;

    [Space]

    [SerializeField] private Image _pausePanel;

    private Text _restartButtonText;
    private Text _resumeButtonText;
    private Text _pauseButtonText;

    private void Awake()
    {
        InitButtonText();
        AddListeners();
    }

    private void InitButtonText()
    {
        _pauseButtonText = _pauseButton.GetComponentInChildren<Text>();
        _restartButtonText = _restartButton.GetComponentInChildren<Text>();
        _resumeButtonText = _resumeButton.GetComponentInChildren<Text>();
    }

    private void AddListeners()
    {
        _pauseButton.onClick.AddListener(PauseButton);
        _restartButton.onClick.AddListener(RestartButton);
        _resumeButton.onClick.AddListener(ResumeButton);
    }

    private void PauseButton()
    {
        Time.timeScale = 0f;
        _pausePanel.gameObject.SetActive(true);
        _pausePanel.DOFade(0.5f, 1f).SetUpdate(true);

        _resumeButton.image.DOFade(1f, 1f);
        _resumeButtonText.DOFade(1f, 1f);
        
        _restartButton.image.DOFade(1f, 1f);
        _restartButtonText.DOFade(1f, 1f);
    }

    private void RestartButton()
    {
        Time.timeScale = 1f;
        SceneChanger.ChangeScene(SceneManager.GetActiveScene().name);
    }

    public void GameStarted()
    {
        _pauseButton.gameObject.SetActive(true);
        _pauseButton.image.DOFade(1f, 1f);
        _pauseButtonText.DOFade(1f, 1f);
    }

    private void ResumeButton()
    {
        Time.timeScale = 1f;
        _pausePanel.DOFade(0f, 0f);
        _pausePanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        LevelManager.OnGameStarted += GameStarted;
    }

    private void OnDisable()
    {
        LevelManager.OnGameStarted -= GameStarted;
    }
}
