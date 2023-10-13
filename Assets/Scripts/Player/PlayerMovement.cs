using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private int _currentLane;

    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _laneWidth;

    private void Awake()
    {
        _currentLane = 2;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
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

        transform.DOMoveX(targetX, 0.5f);

        _currentLane = newLane;
    }

}
