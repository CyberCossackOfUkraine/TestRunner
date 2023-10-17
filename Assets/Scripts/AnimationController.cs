using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private PlayerStateController _playerStateController;

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
            _animator.Play(_animMaps[animNumber]);
            
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
