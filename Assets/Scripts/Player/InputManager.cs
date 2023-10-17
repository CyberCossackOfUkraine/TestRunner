using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private Vector2 _startPos;

    private bool _swipeLeft;
    private bool _swipeRight;
    private bool _swipeUp;
    private bool _swipeDown;

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
    public bool SwipeUp()
    {
        return _swipeUp;
    }
    public bool SwipeDown()
    {
        return _swipeDown;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        _swipeLeft = _swipeRight = _swipeUp = _swipeDown = false;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        switch(touch.phase)
        {
            case TouchPhase.Began:
                _startPos = touch.position;
                break;

            case TouchPhase.Ended:
                Vector3 deltaSwipe = touch.position - _startPos;

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
                break;

        }
        /*
        if (touch.phase == TouchPhase.Ended)
        {
            Vector2 deltaSwipe = touch.deltaPosition;
            if (Mathf.Abs(deltaSwipe.x) > Mathf.Abs(deltaSwipe.y))
            {
                _swipeLeft |= deltaSwipe.x < 0;
                _swipeRight |= deltaSwipe.x > 0;
            } else
            {
                _swipeUp |= deltaSwipe.y > 0;
                _swipeDown |= deltaSwipe.y < 0;

            }
        }
        */

        
    }

}

