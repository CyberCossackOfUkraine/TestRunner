using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Image _gameOverPanel;

    [SerializeField] private Button _restartGameButton;
    [SerializeField] private Text _restartGameButtonText;

    [SerializeField] private Button _watchAdButton;
    [SerializeField] private Text _watchAdButtonText;

    private void Awake()
    {
        HidePanel();
        AddListeners();
    }

    private void HidePanel()
    {
        _gameOverPanel.DOFade(0f, 0f);
        _restartGameButton.image.DOFade(0f, 0f);
        _restartGameButtonText.DOFade(0f, 0f);
        _watchAdButton.image.DOFade(0f, 0f);
        _watchAdButtonText.DOFade(0f, 0f);

        _gameOverPanel.gameObject.SetActive(false);

    }

    private void ShowPanel()
    {
        _gameOverPanel.gameObject.SetActive(true);

        _gameOverPanel.DOFade(0.5f, 1f);
        _restartGameButton.image.DOFade(1f, 1f);
        _restartGameButtonText.DOFade(1f, 1f);
        _watchAdButton.image.DOFade(1f, 1f);
        _watchAdButtonText.DOFade(1f, 1f);
    }

    private void AddListeners()
    {
        _restartGameButton.onClick.AddListener(delegate { SceneChanger.ChangeScene(SceneManager.GetActiveScene().name); });
        _watchAdButton.onClick.AddListener(delegate { AdMobScript.ShowRewardedAd(); });
    }

    private void OnEnable()
    {
        PlayerStateDead.OnPlayerDied += PlayerDied;
        AdMobScript.OnRewardEarned += ResurrectPlayer;
    }

    private void OnDisable()
    {
        PlayerStateDead.OnPlayerDied -= PlayerDied;
        AdMobScript.OnRewardEarned -= ResurrectPlayer;
    }

    private void ResurrectPlayer()
    {
        HidePanel();
        PlayerStateController.instance.SetStateRun();
    }

    private void PlayerDied()
    {
        ShowPanel();
    }
}
