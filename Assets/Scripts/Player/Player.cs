using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public int currentLane;

    public float currentSpeed;
    public float acceleration;
    public float laneWidth;

    public static Player instance;

    private void Awake()
    {
        InitVars();
    }

    private void InitVars()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }

        characterController = GetComponent<CharacterController>();
        currentLane = 2;
    }
}
