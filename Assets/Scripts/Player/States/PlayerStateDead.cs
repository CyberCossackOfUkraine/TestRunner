using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDead : IPlayerState
{
    public void Enter()
    {
        Debug.Log("Enter Dead State");
        AnimationController.instance.SetAnimation(3);
    }

    public void Exit()
    {
        Debug.Log("Exit Dead State");
    }

    public void Update()
    {
        Debug.Log("Update Dead State");
    }
}
