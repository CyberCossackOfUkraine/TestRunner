public class PlayerStateSlide : IPlayerState
{
    private AnimationController _animationController;
    public PlayerStateSlide(AnimationController animationController)
    {
        _animationController = animationController;
    }
    public void Enter()
    {
        _animationController.SetAnimation(3);
    }

    public void Exit()
    {

    }
    public void Update()
    {

    }

}
