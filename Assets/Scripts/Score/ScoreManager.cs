using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private IScoreCalculator _scoreCalculator;
    private Transform _playerPosition;
    private int _currentScore;

    private void Start()
    {
        HideScore();
        _scoreCalculator = new BasicScoreCalculator();
        _playerPosition = Singleton.Instance.PlayerStateController.transform;
    }

    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        _currentScore = _scoreCalculator.CalculateScore(_playerPosition.position.z);
        _scoreText.text = "Score: " + _currentScore;
        
    }

    private void UpdateDatabaseScore()
    {
        Singleton.Instance.DatabaseManager.GetUserHighscore((score) =>
        {
            if (score < _currentScore)
            {
                Singleton.Instance.DatabaseManager.SetDatabaseScore(_currentScore);
            }

        });
    }

    private void OnEnable()
    {
        LevelManager.OnGameStarted += ShowScore;
        PlayerStateDead.OnPlayerDied += UpdateDatabaseScore;
    }

    private void OnDisable()
    {
        LevelManager.OnGameStarted -= ShowScore;
        PlayerStateDead.OnPlayerDied += UpdateDatabaseScore;
    }


    private void ShowScore()
    {
        _scoreText.DOFade(1f, 1f);
    }

    private void HideScore()
    {
        _scoreText.DOFade(0f, 0f);
    }
}
