using System.Collections;
using UnityEngine;

public class PlayerStateJump : IPlayerState
{
    private AnimationController _animationController;

    private Coroutine _coroutine;

    public PlayerStateJump(AnimationController animationController)
    {
        _animationController = animationController;
    }

    public void Enter()
    {
        _coroutine = Singleton.Instance.CoroutineProxy.StartProxyCoroutine(PlayJumpAnimation());
    }

    public void Exit()
    {
        Singleton.Instance.CoroutineProxy.StopProxyCoroutine(_coroutine);
    }

    public void Update()
    {
    }
    private IEnumerator PlayJumpAnimation()
    {
        _animationController.SetAnimation(4);
        float duration = _animationController.GetAnimationLength(4);
        yield return new WaitForSeconds(duration);
        Singleton.Instance.PlayerStateController.SetStateRun();
    }
}
