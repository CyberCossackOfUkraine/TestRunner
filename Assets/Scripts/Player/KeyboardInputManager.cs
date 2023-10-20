using UnityEngine;

public class KeyboardInputManager : IControlStrategy
{
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

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _up = true;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _down = true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _left = true;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _right = true;
        }


    }
}

