using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private PlayerMovement _playerMovement;

    private Dictionary<Type, IPlayerState> _statesMap;
    private IPlayerState _stateCurrent;

    private bool _isPlayerImmortal;

    private void Awake()
    {
        InitStates();
    }

    private void Start()
    {
        SetStateByDefault();
    }

    private void Update()
    {
        if (_stateCurrent != null)
        {
            _stateCurrent.Update();
        }
    }

    private void InitStates()
    {
        _statesMap = new Dictionary<Type, IPlayerState>();
        _statesMap[typeof(PlayerStateRun)] = new PlayerStateRun(_animationController);
        _statesMap[typeof(PlayerStateIdle)] = new PlayerStateIdle();
        _statesMap[typeof(PlayerStateDead)] = new PlayerStateDead();
        _statesMap[typeof(PlayerStateJump)] = new PlayerStateJump(_animationController);
        _statesMap[typeof(PlayerStateSlide)] = new PlayerStateSlide(_animationController);
    }

    private void SetState(IPlayerState newState)
    {
        if (_stateCurrent != null)
        {
            _stateCurrent.Exit();
        }

        _stateCurrent = newState;
        _stateCurrent.Enter();

    }

    private void SetStateByDefault()
    {
        SetStateIdle();
    }

    private IPlayerState GetState<T>() where T : IPlayerState
    {
        var type = typeof(T);
        return _statesMap[type];
    }

    public void SetStateIdle()
    {
        var type = GetState<PlayerStateIdle>();
        SetState(type);
    }

    public void SetStateRun()
    {
        var type = GetState<PlayerStateRun>();
        SetState(type);
    }

    public void SetStateDead()
    {
        var type = GetState<PlayerStateDead>();
        SetState(type);
    }
    public void SetStateJump()
    {
        var type = GetState<PlayerStateJump>();
        SetState(type);
    }
    public void SetStateSlide()
    {
        var type = GetState<PlayerStateSlide>();
        SetState(type);
    }

    public void SetPlayerImmortal(int seconds)
    {
        _isPlayerImmortal = true;
        Invoke("ResetImmortality", seconds);
    }

    private void ResetImmortality()
    {
        _isPlayerImmortal = false;
    }

    public bool IsPlayerImmortal()
    {
        return _isPlayerImmortal;
    }

    public Type GetCurrentStateType()
    {
        if (_stateCurrent == null)
        {
            return null;
        }

        return _stateCurrent.GetType();
    }
}
