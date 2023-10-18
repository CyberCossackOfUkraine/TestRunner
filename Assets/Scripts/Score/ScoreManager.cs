using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private IScoreCalculator _scoreCalculator;
    private Transform _playerPosition;
    private int _currentScore;

    private void Start()
    {
        _scoreCalculator = new BasicScoreCalculator();
        _playerPosition = PlayerStateController.instance.transform;
    }

    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        _currentScore += _scoreCalculator.CalculateScore(_playerPosition.position.z);
        _scoreText.text = "Score: " + _currentScore;
    }
}
