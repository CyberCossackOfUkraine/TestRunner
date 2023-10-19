public class PlayerStateJump : IPlayerState
{
    private AnimationController _animationController;

    public PlayerStateJump(AnimationController animationController)
    {
        _animationController = animationController;
    }

    public void Enter()
    {
        _animationController.SetAnimation(4);
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}
