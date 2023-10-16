using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Text _startButtonText;

    private void Awake()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        PlayerStateController.instance.SetStateActive();
        _startButton.image.DOFade(0f, 1f);
        _startButtonText.DOFade(0f, 1f);
    }

}
