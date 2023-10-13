using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

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

        if (touch.phase == TouchPhase.Ended)
        {
            Vector2 deltaSwipe = touch.deltaPosition;
            if (Mathf.Abs(deltaSwipe.x) > Mathf.Abs(deltaSwipe.y))
            {
                _swipeLeft |= deltaSwipe.x < 0;
                _swipeRight |= deltaSwipe.x > 0;
            }
        }

        
    }

}

