using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class LevelManager : MonoBehaviour
{
    // Buttons and Text
    [SerializeField] private Button _startButton;
    [SerializeField] private Text _startButtonText;
    [Space]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Text _restartButtonText;
    [Space]
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Text _pauseButtonText;
    [Space]
    [SerializeField] private Button _logOutButton;
    [SerializeField] private Text _logOutButtonText;
    [Space]
    [SerializeField] private Button _resumeButton;
    [Space]
    [SerializeField] private Button _pauseRestartButton;


    // Panels
    [SerializeField] private Image _pausePanel;

    private void Awake()
    {

        _startButton.onClick.AddListener(StartGame);
        _restartButton.onClick.AddListener(Restart);
        _pauseButton.onClick.AddListener(PauseButton);
        _logOutButton.onClick.AddListener(LogOut);
        _resumeButton.onClick.AddListener(ResumeButton);
        _pauseRestartButton.onClick.AddListener(Restart);

        _pauseButton.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
    }

    private void StartGame()
    {
        PlayerStateController.instance.SetStateRun();
        _startButton.image.DOFade(0f, 1f);
        _startButtonText.DOFade(0f, 1f).OnComplete(delegate { _startButton.gameObject.SetActive(false); });

        _logOutButton.image.DOFade(0f, 1f);
        _logOutButtonText.DOFade(0f, 1f).OnComplete(delegate { _logOutButton.gameObject.SetActive(false); });

        _pauseButton.gameObject.SetActive(true);
        _pauseButton.image.DOFade(1f, 1f);
        _pauseButtonText.DOFade(1f, 1f);


    }

    private void LogOut()
    {
        Debug.Log("Username: " + FirebaseAuth.DefaultInstance.CurrentUser.DisplayName);
        FirebaseAuth.DefaultInstance.SignOut();
        SceneChanger.ChangeScene("AuthScene");
    }

    private void PauseButton()
    {
        Time.timeScale = 0f;
        _pausePanel.gameObject.SetActive(true);
        _pausePanel.DOFade(0.5f, 1f).SetUpdate(true);
    }

    private void ResumeButton()
    {
        Time.timeScale = 1f;
        _pausePanel.DOFade(0f, 0f);
        _pausePanel.gameObject.SetActive(false);
    }

    private void Restart()
    {
        SceneChanger.ChangeScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        PlayerStateDead.OnPlayerDied += GameOver;
    }

    private void OnDisable()
    {
        PlayerStateDead.OnPlayerDied -= GameOver;
    }

    private void GameOver()
    {
        _restartButton.gameObject.SetActive(true);
        _restartButton.image.DOFade(1f, 1f);
        _restartButtonText.DOFade(1f, 1f);
    }

}
