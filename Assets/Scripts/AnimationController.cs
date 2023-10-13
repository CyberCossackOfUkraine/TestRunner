using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private Dictionary<int, string> AnimState = new Dictionary<int, string>
    {
        {1, "PlayerIdle" },
        {2, "PlayerRun" }
    };

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.Play(AnimState[2]);
    }
}
