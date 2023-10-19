using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    [SerializeField] private AnimationController _animationController;
    [SerializeField] private DatabaseManager _databaseManager;
    [SerializeField] private PlayerStateController _playerStateController;
    [SerializeField] private ZoneMover _zoneMover;

    public AnimationController AnimationController => _animationController;
    public DatabaseManager DatabaseManager => _databaseManager;
    public PlayerStateController PlayerStateController => _playerStateController;
    public ZoneMover ZoneMover => _zoneMover;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
