using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : IPlayerState
{
    public void Enter()
    {
        AnimationController.instance.SetAnimation(1);
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}
