using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControlStrategy
{
    void HandleInput();

    bool Left();

    bool Right();

    bool Up();

    bool Down();
}
