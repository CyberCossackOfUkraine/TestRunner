using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Firebase.Auth;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button _logOutButton;
    [SerializeField] private Button _scoreboardButton;

    [Space] 
    [SerializeField] private GameObject _scoreboardPanel;

    private Text _logOutButtonText;
    private Text _scoreboardButtonText;

    private bool _isGameStarted;

    public delegate void GameStarted();
    public static event GameStarted OnGameStarted;

    private void Awake()
    {
        InitButtonText();

        _logOutButton.onClick.AddListener(LogOut);
        _scoreboardButton.onClick.AddListener(OpenScoreboard);
    }

    private void Update()
    {
        if (_isGameStarted)
            return;

        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            StartGame();
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(touch.fingerId)) 
            {
                StartGame();
            }
        }
    }

    private void InitButtonText()
    {
        _logOutButtonText = _logOutButton.GetComponentInChildren<Text>();
        _scoreboardButtonText = _scoreboardButton.GetComponentInChildren<Text>();

    }

    private void OpenScoreboard()
    {
        _scoreboardPanel.SetActive(true);
    }

    private void StartGame()
    {
        Singleton.Instance.PlayerStateController.SetStateRun();
        OnGameStarted?.Invoke();

        _logOutButton.image.DOFade(0f, 1f);
        _logOutButtonText.DOFade(0f, 1f).OnComplete(delegate { _logOutButton.gameObject.SetActive(false); });

        _scoreboardButton.image.DOFade(0f, 1f);
        _scoreboardButtonText.DOFade(0f, 1f).OnComplete(delegate { _scoreboardButton.gameObject.SetActive(false); });

        _scoreboardPanel.SetActive(false);
    }

    private void LogOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        SceneChanger.ChangeScene("AuthScene");
    }

}
