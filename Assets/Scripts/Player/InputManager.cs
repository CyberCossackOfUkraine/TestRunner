using UnityEngine;

public class InputManager : IControlStrategy
{
    private Vector2 _startPos = Vector2.zero;

    private bool _swipeRegistered;

    private bool _swipeLeft;
    private bool _swipeRight;
    private bool _swipeUp;
    private bool _swipeDown;

    public bool Left() => _swipeLeft;

    public bool Right() => _swipeRight;
    public bool Up() => _swipeUp;
    public bool Down() => _swipeDown;

    public void HandleInput()
    {
        _swipeLeft = _swipeRight = _swipeUp = _swipeDown = false;

        if (Time.timeScale == 0f)
            return;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        switch(touch.phase)
        {
            case TouchPhase.Began:
                _startPos = touch.position;
                _swipeRegistered = false;
                break;

            case TouchPhase.Moved:
                if (_swipeRegistered)
                    return;
                Vector2 deltaSwipe = touch.position - _startPos;
                if (Mathf.Abs(deltaSwipe.x) > Mathf.Abs(deltaSwipe.y))
                {
                    _swipeLeft |= deltaSwipe.x < 0;
                    _swipeRight |= deltaSwipe.x > 0;
                }
                else
                {
                    _swipeUp |= deltaSwipe.y > 0;
                    _swipeDown |= deltaSwipe.y < 0;
                }
                _swipeRegistered = true;
                break;

            case TouchPhase.Ended:
                _swipeRegistered = false;
                break;

        }
        
    }

}

