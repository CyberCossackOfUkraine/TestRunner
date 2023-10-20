using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SettingsScriptableObject _settings;
    [SerializeField] private int _currentLane;
    [SerializeField] private float _laneWidth;
    [SerializeField][Range(0,1)] private float _sideMoveDuration;

    private CharacterController _characterController;
    private PlayerStateController _playerStateController;

    private IControlStrategy _inputController;

    private float _maxSpeed;
    private float _currentSpeed;
    private float _acceleration;
    private float _forwardSpeed;
    private bool _isMovingSide;

    public bool canMove;
    public float CurrentSpeed() => _currentSpeed;

    private void Awake()
    {
        InitVars();
        #if UNITY_EDITOR
            _inputController = new KeyboardInputManager();
        #elif UNITY_ANDROID
            _inputController = new InputManager();
        #endif


    }

    private void InitVars()
    {
        _characterController = GetComponent<CharacterController>();
        _playerStateController = GetComponent<PlayerStateController>();
        _currentSpeed = _settings._playerStartSpeed;
        _maxSpeed = _settings._playerMaxSpeed;
        _acceleration = _settings._playerSpeedAcceleration;
    }

    public void Update()
    {
        if(!canMove)
        {
            return;
        }

        _inputController.HandleInput();

        MoveForward();
        if (!_isMovingSide)
            HandleSwipeInput();
    }

    private void MoveForward()
    {
        _forwardSpeed = _currentSpeed * Time.deltaTime;
        Vector3 move = new Vector3(0f, 0f, _forwardSpeed);
        _characterController.Move(move);
        if (_currentSpeed <  _maxSpeed)
            _currentSpeed += _acceleration * Time.deltaTime;
    }

    private void HandleSwipeInput()
    {
        if (_inputController.Left())
        {
            ChangeLane(-1);
        }
        else if (_inputController.Right())
        {
            ChangeLane(1);
        }
        else if (_inputController.Down())
        {
            Slide();
        }
        else if (_inputController.Up())
        {
            Jump();
        }
    }

    private void Jump()
    {
        _playerStateController.SetStateJump();
    }

    private void Slide()
    {
        _playerStateController.SetStateSlide();
    }

    private void ChangeLane(int direction)
    {
        int newLane = Mathf.Clamp(_currentLane + direction, 1, 3);
        float targetX = newLane * _laneWidth;

        Vector3 targetPosition = new Vector3(targetX, _characterController.transform.position.y, _characterController.transform.position.z);

        StartCoroutine(MoveX(targetPosition));

        _currentLane = newLane;
    }

    private IEnumerator MoveX(Vector3 targetPosition)
    {
        _isMovingSide = true;
        Vector3 startPosition = _characterController.transform.position;
        float elapsed = 0;

        while (elapsed < _sideMoveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _sideMoveDuration);
            Vector3 nextPosition = Vector3.Lerp(startPosition, targetPosition, t);
            nextPosition.z = _characterController.transform.position.z + _forwardSpeed * Time.deltaTime;
            Vector3 sideMove = nextPosition - _characterController.transform.position;

            _characterController.Move(sideMove);
            if (_currentSpeed < _maxSpeed)
                _currentSpeed += _acceleration * Time.deltaTime;
            _forwardSpeed = _currentSpeed * Time.deltaTime;
            yield return null;

        }
        _isMovingSide = false;
    }
    private void OnEnable()
    {
        PlayerStateDead.OnPlayerDied += StopMoving;
        LevelManager.OnGameStarted += EnableMoving;
        AdMobScript.OnRewardEarned += EnableMoving;
    }

    private void OnDisable()
    {
        PlayerStateDead.OnPlayerDied -= StopMoving;
        LevelManager.OnGameStarted -= EnableMoving;
        AdMobScript.OnRewardEarned -= EnableMoving;
    }

    public void StopMoving()
    {
        canMove = false;
    }

    public void EnableMoving()
    {
        canMove = true;
    }
}
