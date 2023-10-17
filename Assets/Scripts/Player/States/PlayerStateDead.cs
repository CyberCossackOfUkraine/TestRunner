using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDead : IPlayerState
{
    public delegate void PlayerDied();
    public static event PlayerDied OnPlayerDied;
    public void Enter()
    {
        AnimationController.instance.SetAnimation(5);
        OnPlayerDied?.Invoke();
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }
}
