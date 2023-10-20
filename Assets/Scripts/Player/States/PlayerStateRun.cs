using System.Collections;
using UnityEngine;

public class PlayerStateRun : IPlayerState
{
    private AnimationController _animationController;
    public PlayerStateRun(AnimationController animationController) { 
        _animationController = animationController;
    }

    public void Enter()
    {
        _animationController.SetAnimation(2);
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }

}
