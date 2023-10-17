using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerStateController _playerStateController;
    private AnimationController _animationController;
    private CoroutineProxy _coroutineProxy;

    [SerializeField] private int _currentLane;

    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _laneWidth;
    private float _forwardSpeed;
    private bool _isMovingSide;

    public bool canMove;

    private void Awake()
    {
        InitVars();
    }

    private void InitVars()
    {
        _characterController = GetComponent<CharacterController>();
        _playerStateController = GetComponent<PlayerStateController>();
        _animationController = GetComponentInChildren<AnimationController>();
        _coroutineProxy = GetComponent<CoroutineProxy>();
    }

    public void Update()
    {
        if(!canMove)
        {
            return;
        }

        MoveForward();
        if (!_isMovingSide)
            HandleSwipeInput();
    }

    private void MoveForward()
    {
        _forwardSpeed = _currentSpeed * Time.deltaTime;
        Vector3 move = new Vector3(0f, 0f, _forwardSpeed);
        _characterController.Move(move);
        _currentSpeed += _acceleration * Time.deltaTime;
    }

    private void HandleSwipeInput()
    {
        if (InputManager.instance.SwipeLeft())
        {
            ChangeLane(-1);
        }
        else if (InputManager.instance.SwipeRight())
        {
            ChangeLane(1);
        }
        else if (InputManager.instance.SwipeDown())
        {
            Slide();
        }
        else if (InputManager.instance.SwipeUp())
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
        float duration = 0.1f;
        Vector3 startPosition = _characterController.transform.position;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            Vector3 nextPosition = Vector3.Lerp(startPosition, targetPosition, t);
            nextPosition.z = _characterController.transform.position.z + _forwardSpeed * Time.deltaTime;
            Vector3 sideMove = nextPosition - _characterController.transform.position;

            _characterController.Move(sideMove);
            _currentSpeed += _acceleration * Time.deltaTime;
            _forwardSpeed = _currentSpeed * Time.deltaTime;
            yield return null;

        }
        _isMovingSide = false;
    }

    private void OnEnable()
    {
        PlayerStateDead.OnPlayerDied += StopMoving;
    }

    private void OnDisable()
    {
        PlayerStateDead.OnPlayerDied -= StopMoving;
    }

    public void StopMoving()
    {
        canMove = false;
    }
}
