using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _logOutButton;

    private Text _startButtonText;
    private Text _logOutButtonText;

    public delegate void GameStarted();
    public static event GameStarted OnGameStarted;

    private void Awake()
    {
        InitButtonText();

        _startButton.onClick.AddListener(StartGame);
        _logOutButton.onClick.AddListener(LogOut);
    }

    private void InitButtonText()
    {
        _startButtonText = _startButton.GetComponentInChildren<Text>();
        _logOutButtonText = _logOutButton.GetComponentInChildren<Text>();

    }

    private void StartGame()
    {
        PlayerStateController.instance.SetStateRun();
        OnGameStarted?.Invoke();
        Debug.Log("GameStartedInvoke");
        _startButton.image.DOFade(0f, 1f);
        _startButtonText.DOFade(0f, 1f).OnComplete(delegate { _startButton.gameObject.SetActive(false); });

        _logOutButton.image.DOFade(0f, 1f);
        _logOutButtonText.DOFade(0f, 1f).OnComplete(delegate { _logOutButton.gameObject.SetActive(false); });
    }

    private void LogOut()
    {
        Debug.Log("Username: " + FirebaseAuth.DefaultInstance.CurrentUser.DisplayName);
        FirebaseAuth.DefaultInstance.SignOut();
        SceneChanger.ChangeScene("AuthScene");
    }

}
