public class PlayerStateRun : IPlayerState
{
    private AnimationController _animationController;
    private PlayerMovement _playerMovement;
    public PlayerStateRun(AnimationController animationController, PlayerMovement playerMovement) { 
        _animationController = animationController;
        _playerMovement = playerMovement;
    }

    public void Enter()
    {
        _animationController.SetAnimation(2);
        _playerMovement.canMove = true;
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }

}
