using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private PlayerStateController _playerStateController;
    [SerializeField][Range(0,1)] private float _transitionSmoothness;

    private Animator _animator;
    private RuntimeAnimatorController _animController;

    private Dictionary<int, string> _animMaps;
    private Dictionary<string, float> _animLengthMaps;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animController = _animator.runtimeAnimatorController;
        InitAnimMaps();
        InitAnimLengthMaps();
    }

    private void InitAnimMaps()
    {
        _animMaps = new Dictionary<int, string>
        {
            {1, "PlayerIdle" },
            {2, "PlayerRun" },
            {3, "PlayerSlide" },
            {4, "PlayerJump" },
            {5, "PlayerDead" }
        };
    }
    private void InitAnimLengthMaps()
    {
        _animLengthMaps = new Dictionary<string, float>();
        for(int i = 0; i < _animController.animationClips.Length; i++)
        {
            _animLengthMaps[_animController.animationClips[i].name] = _animController.animationClips[i].length;
        }
    }

    public void SetAnimation(int animNumber)
    {
        if (animNumber <= _animMaps.Count && animNumber > 0)
        {
            _animator.CrossFade(_animMaps[animNumber], _transitionSmoothness);
        } else
        {
            Debug.Log("Incorrect Animation Number");
        }
    }

    public float GetAnimationLength(int animNumber)
    {
        string animName = _animMaps[animNumber];
        float animLength = _animLengthMaps[animName];
        return animLength;
    }

    public void BackToRunState()
    {
        _playerStateController.SetStateRun();
    }
}
