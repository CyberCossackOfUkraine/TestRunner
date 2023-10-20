using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private PlayerStateController _playerStateController;
    [SerializeField][Range(0,1)] private float _transitionSmoothness;

    private Animator _animator;

    private Dictionary<int, string> _animMaps;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        InitAnimMaps();
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

    public void BackToRunState()
    {
        _playerStateController.SetStateRun();
    }
}
