using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    private bool _swipeLeft;
    private bool _swipeRight;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
    }

    public bool SwipeLeft()
    {
        return _swipeLeft;
    }

    public bool SwipeRight()
    {
        return _swipeRight;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        _swipeLeft = _swipeRight = false;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                _startTouchPosition = touch.position;
                break;
            case TouchPhase.Moved:
                _endTouchPosition = touch.position;
                break;
            case TouchPhase.Ended:
                _endTouchPosition = touch.position;
                DetectInput();
                break;
        }
    }

    private void DetectInput()
    {
        float deltaX = _endTouchPosition.x - _startTouchPosition.x;
        float deltaY = _endTouchPosition.y - _startTouchPosition.y;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            _swipeLeft = deltaX < 0;
            _swipeRight = deltaX > 0;
            Debug.Log("Swipe Left: " + _swipeLeft + "\nSwipe Right: " + _swipeRight);
        }
    }

}

