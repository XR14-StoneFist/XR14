using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum HangWallState { None, Left, Right }
public class PlayerStateMachine : MonoBehaviour
{
    public Animator Animator;
    public GameObject Character;
    
    [Header("점프")]
    public SubCollider JumpBox;
    public float JumpPower;
    public float CoyoteTime;
    public float DoubleJumpTimeout;
    public ParticleSystem JumpEffect;
    public ParticleSystem DoubleJumpEffect;

    [Header("이동")]
    public float MoveSpeed;
    public float AccelSpeed;
    public ParticleSystem RunEffect;

    [Header("벽차기")]
    public SubCollider ClimbWallLeft;
    public SubCollider ClimbWallRight;
    public PhysicMaterial PhysicMaterial;
    public float WallJumpPower;

    [Header("대시")]
    public float DashPower;
    public float DashDuration;
    public GameObject DashWrap;
    public ParticleSystem DashEffect;
    
    public Rigidbody Rigidbody { get; private set; }
    
    public bool IsGrounded { get; private set; }
    public float VelocityX { get; set; }
    public bool CanJump { get; set; }
    public bool CanDoubleJump { get; set; }
    public float CoyoteTimeCounter { get; set; }
    public float DoubleJumpTimeoutDelta { get; set; }
    public HangWallState hangWallState { get; set; }
    public GameObject DashFlame { get; set; }
    public Vector2 StartMousePosition { get; set; }
    public Vector2 EndMousePosition { get; set; }
    public GameObject DashArrowObject { get; set; }
    
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
            if (tagName == "ground")
            {
                IsGrounded = true;
            }
        };
		
        JumpBox.OnTriggerExitAction += tagName =>
        {
            if (tagName == "ground")
            {
                IsGrounded = false;
            }
        };

        ClimbWallLeft.OnTriggerEnterAction += tagName =>
        {
            if (tagName == "wall")
            {
                hangWallState = HangWallState.Left;
                Rigidbody.useGravity = false;
                Rigidbody.velocity = Vector3.zero;
            }
        };
        
        ClimbWallLeft.OnTriggerExitAction += tagName =>
        {
            if (tagName == "wall")
            {
                hangWallState = HangWallState.None;
                Rigidbody.useGravity = true;
            }
        };
        
        ClimbWallRight.OnTriggerEnterAction += tagName =>
        {
            if (tagName == "wall")
            {
                hangWallState = HangWallState.Right;
                Rigidbody.useGravity = false;
                Rigidbody.velocity = Vector3.zero;
            }
        };
        
        ClimbWallRight.OnTriggerExitAction += tagName =>
        {
            if (tagName == "wall")
            {
                hangWallState = HangWallState.None;
                Rigidbody.useGravity = true;
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
        if (!Rigidbody.isKinematic && _currentState.GetType().Name != "PlayerDashState")
        {
            if (!Input.GetKey(KeyBind.MoveLeftKeyCode) ^ Input.GetKey(KeyBind.MoveRightKeyCode))
            {
                VelocityX = Mathf.Lerp(VelocityX, 0, AccelSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyBind.MoveLeftKeyCode))
            {
                VelocityX = Mathf.Lerp(VelocityX, -MoveSpeed, AccelSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyBind.MoveRightKeyCode))
            {
                VelocityX = Mathf.Lerp(VelocityX, MoveSpeed, AccelSpeed * Time.deltaTime);
            }

            var velocity = Rigidbody.velocity;
            velocity = new Vector3(VelocityX, velocity.y, velocity.z);
            Rigidbody.velocity = velocity;

            if (VelocityX > 0)
            {
                Character.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (VelocityX < 0)
            {
                Character.transform.rotation = Quaternion.Euler(0, 270, 0);
            }
        }
    }
    
    public void DestroyDashArrowObject()
    {
        Destroy(DashArrowObject);
    }
    
    public float EaseOutQuint(float value)
    { 
        return 1 - Mathf.Pow(1 - value, 5);
    }
}
