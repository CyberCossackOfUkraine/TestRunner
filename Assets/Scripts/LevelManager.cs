using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Text _startButtonText;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Text _restartButtonText;

    private void Awake()
    {
        _startButton.onClick.AddListener(StartGame);
        _restartButton.onClick.AddListener(Restart);

        _restartButton.image.DOFade(0f, 0f);
        _restartButtonText.DOFade(0f, 0f);
        _restartButton.gameObject.SetActive(false);
    }

    private void DisableButton()
    {
        _startButton.gameObject.SetActive(false);
    }

    private void StartGame()
    {
        PlayerStateController.instance.SetStateRun();
        _restartButton.image.DOFade(0f, 1f);
        _restartButtonText.DOFade(0f, 1f).OnComplete(DisableButton);
    }

    private void Restart()
    {
        Debug.Log("Restart");
        SceneChanger.ChangeScene("LevelScene");
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
