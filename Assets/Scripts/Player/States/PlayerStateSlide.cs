using System.Collections;
using UnityEngine;

public class PlayerStateSlide : IPlayerState
{
    private AnimationController _animationController;
    
    private Coroutine _coroutine;
    public PlayerStateSlide(AnimationController animationController)
    {
        _animationController = animationController;
    }
    public void Enter()
    {
        _coroutine = Singleton.Instance.CoroutineProxy.StartProxyCoroutine(PlaySlideAnimation());
    }

    public void Exit()
    {
        Singleton.Instance.CoroutineProxy.StopProxyCoroutine(_coroutine);
    }

    public void Update()
    {

    }

    private IEnumerator PlaySlideAnimation()
    {
        _animationController.SetAnimation(3);
        float duration = _animationController.GetAnimationLength(3);
        yield return new WaitForSeconds(duration);
        Singleton.Instance.PlayerStateController.SetStateRun();
    }

}
