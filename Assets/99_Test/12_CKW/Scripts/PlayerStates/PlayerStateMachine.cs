using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("점프")]
    public SubCollider JumpBox;
    public float JumpPower;
    public float CoyoteTime;
    public float DoubleJumpTimeout;
    
    public Rigidbody Rigidbody { get; private set; }
    
    public bool IsGrounded { get; private set; }
    public bool CanJump { get; set; }
    public bool CanDoubleJump { get; set; }
    public float CoyoteTimeCounter { get; set; }
    public float DoubleJumpTimeoutDelta { get; set; }
    
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    
    public PlayerBaseState CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        JumpBox.OnTriggerEnterAction += tagName =>
        {
            if (tagName == "Ground")
            {
                IsGrounded = true;
            }
        };
		
        JumpBox.OnTriggerExitAction += tagName =>
        {
            if (tagName == "Ground")
            {
                IsGrounded = false;
            }
        };
        
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState.UpdateState();
    }
}
