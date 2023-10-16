using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.InteropServices.WindowsRuntime;

public class PlayerStateActive : IPlayerState
{
    private CharacterController _characterController;
    private Player _player;
    private CoroutineProxy _coroutineProxy;
    private int _currentLane;

    private float _currentSpeed;
    private float _acceleration;
    private float _laneWidth;

    public void Enter()
    {
        Debug.Log("Enter Active State");
        InitVars();
    }

    private void InitVars()
    {
        _player = Player.instance;
        _coroutineProxy = CoroutineProxy.instance;
        _characterController = _player.characterController;
        _currentLane = _player.currentLane;
        _currentSpeed = _player.currentSpeed;
        _acceleration = _player.acceleration;
        _laneWidth = _player.laneWidth;
        AnimationController.instance.SetAnimation(2);
    }

    public void Exit()
    {
        Debug.Log("Exit Active State");
    }

    public void Update()
    {
        Debug.Log("Update Active State");
        MoveForward();
        HandleSwipeInput();
    }

    private void MoveForward()
    {
        float forwardSpeed = _currentSpeed * Time.deltaTime;
        Vector3 move = new Vector3(0f, 0f, forwardSpeed);
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
    }

    private void ChangeLane(int direction)
    {
        Debug.Log("Change Lane. Direction: " + direction + "\nCurrent Lane: " + _currentLane);
        int newLane = Mathf.Clamp(_currentLane + direction, 1, 3);
        float targetX = newLane * _laneWidth;
        Debug.Log("targetX: " + targetX);
        _characterController.transform.DOMoveX(targetX, 0.5f).OnComplete(Test);
        
        _currentLane = newLane;
    }

    private IEnumerator MoveX(float targetX)
    {
        yield return null;
    }

    private void Test()
    {
        Debug.Log("Completed. Position: " + Player.instance.transform.position);
    }
}
