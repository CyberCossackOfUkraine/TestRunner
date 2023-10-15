using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private Dictionary<int, string> _animMaps;

    public static AnimationController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }

        _animator = GetComponentInChildren<Animator>();
        InitAnimMaps();
    }

    private void InitAnimMaps()
    {
        _animMaps = new Dictionary<int, string>
        {
            {1, "PlayerIdle" },
            {2, "PlayerRun" }
        };
    }

    public void SetAnimation(int animNumber)
    {
        if (animNumber <= _animMaps.Count && animNumber > 0)
        {
            _animator.Play(_animMaps[animNumber]);
        } else
        {
            Debug.Log("no");
        }
    }
}
