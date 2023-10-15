using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : IPlayerState
{
    public void Enter()
    {
        Debug.Log("Enter Idle State");
        AnimationController.instance.SetAnimation(1);
    }

    public void Exit()
    {
        Debug.Log("Exit Idle State");
    }

    public void Update()
    {
        Debug.Log("Update Idle State");
    }
}
