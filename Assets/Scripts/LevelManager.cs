using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Button _startButton;

    private void Awake()
    {
       _startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        PlayerStateController.instance.SetStateActive();
        _startButton.image.DOFade(0f, 1f);
    }

}
