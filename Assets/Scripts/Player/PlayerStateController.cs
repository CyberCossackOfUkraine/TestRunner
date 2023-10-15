using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    private Dictionary<Type, IPlayerState> _statesMap;
    private IPlayerState _stateCurrent;
    public static PlayerStateController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

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
        _statesMap[typeof(PlayerStateActive)] = new PlayerStateActive();
        _statesMap[typeof(PlayerStateIdle)] = new PlayerStateIdle();
        _statesMap[typeof(PlayerStateDead)] = new PlayerStateDead();
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

    public void SetStateActive()
    {
        var type = GetState<PlayerStateActive>();
        SetState(type);
    }

    public void SetStateDead()
    {
        var type = GetState<PlayerStateDead>();
        SetState(type);
    }

}
