using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : IPlayerState
{
    public void Enter()
    {
        Debug.Log("Enter Idle State");
    }

    public void Exit()
    {
        Debug.Log("Enter Idle State");
    }

    public void Update()
    {
        Debug.Log("Update Idle State");
    }
}
