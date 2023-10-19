public class PlayerStateIdle : IPlayerState
{
    public void Enter()
    {
        Singleton.Instance.AnimationController.SetAnimation(1);
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}
