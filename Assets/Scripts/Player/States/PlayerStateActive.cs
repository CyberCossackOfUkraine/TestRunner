using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerStateActive : IPlayerState
{
    private CharacterController _characterController;
    private int _currentLane;

    private float _currentSpeed;
    private float _acceleration;
    private float _laneWidth;

    public void Enter()
    {
        Debug.Log("Enter Active State");
        _characterController = Player.instance.characterController;
        _currentLane = Player.instance.currentLane;
        _currentSpeed = Player.instance.currentSpeed;
        _acceleration = Player.instance.acceleration;
        _laneWidth = Player.instance.laneWidth;
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
        int newLane = Mathf.Clamp(_currentLane + direction, 1, 3);
        float targetX = newLane * _laneWidth;

        _characterController.transform.DOMoveX(targetX, 0.5f);

        _currentLane = newLane;
    }
}
