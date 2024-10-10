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

    [Header("이동")]
    public float MoveSpeed;

    public float AccelSpeed;
    
    public Rigidbody Rigidbody { get; private set; }
    
    public bool IsGrounded { get; private set; }
    public bool CanJump { get; set; }
    public bool CanDoubleJump { get; set; }
    public float CoyoteTimeCounter { get; set; }
    public float DoubleJumpTimeoutDelta { get; set; }
    public GameObject DashFlame { get; set; }
    
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    private float _velocityX = 0f;
    
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
        _currentState = _states.Idle();
        _currentState.EnterState();
    }

    private void Update()
    {
        MoveHorizontal();
        _currentState.UpdateState();
    }

    private void MoveHorizontal()
    {
        if (!Input.GetKey(KeyBind.MoveLeftKeyCode) ^ Input.GetKey(KeyBind.MoveRightKeyCode))
        {
            _velocityX = Mathf.Lerp(_velocityX, 0, AccelSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyBind.MoveLeftKeyCode))
        {
            _velocityX = Mathf.Lerp(_velocityX, -MoveSpeed, AccelSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyBind.MoveRightKeyCode))
        {
            _velocityX = Mathf.Lerp(_velocityX, MoveSpeed, AccelSpeed * Time.deltaTime);
        }

        var velocity = Rigidbody.velocity;
        velocity = new Vector3(_velocityX, velocity.y, velocity.z);
        Rigidbody.velocity = velocity;
    }
}
