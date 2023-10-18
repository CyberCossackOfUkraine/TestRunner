using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : IControlStrategy
{

    private Vector2 _startPos;

    private bool _left;
    private bool _right;
    private bool _up;
    private bool _down;

    public bool Left()
    {
        return _left;
    }

    public bool Right()
    {
        return _right;
    }
    public bool Up()
    {
        return _up;
    }
    public bool Down()
    {
        return _down;
    }

    public void HandleInput()
    {
        _left = _right = _up = _down = false;

        if (Time.timeScale == 0f)
            return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(_up);
            _up = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _down = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _left = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _right = true;
        }


    }
}

