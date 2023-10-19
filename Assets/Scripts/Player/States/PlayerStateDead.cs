public class PlayerStateDead : IPlayerState
{
    public delegate void PlayerDied();
    public static event PlayerDied OnPlayerDied;
    public void Enter()
    {
        Singleton.Instance.AnimationController.SetAnimation(5);
        OnPlayerDied?.Invoke();
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }
}
