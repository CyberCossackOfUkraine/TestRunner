using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDead : IPlayerState
{
    public void Enter()
    {
        Debug.Log("Enter Dead State");
    }

    public void Exit()
    {
        Debug.Log("Enter Dead State");
    }

    public void Update()
    {
        Debug.Log("Update Dead State");
    }
}
