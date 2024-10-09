using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {}
    
    public override void EnterState()
    {
        _context.CanDoubleJump = true;
        _context.CoyoteTimeCounter = _context.CoyoteTime;
    }

    public override void UpdateState()
    {
        Jump();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchStates()
    {
        if (_context.IsGrounded)
            SwitchState(_factory.Grounded());
    }

    private void Jump()
    {
        _context.CoyoteTimeCounter -= Time.deltaTime;
        _context.DoubleJumpTimeoutDelta -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyBind.JumpKeyCode))
        {
            if (_context.CoyoteTimeCounter > 0f && _context.CanJump)
            {
                Vector3 value = _context.Rigidbody.velocity;
                value.y = _context.JumpPower;
                _context.Rigidbody.velocity = value;
                _context.CanJump = false;
                _context.DoubleJumpTimeoutDelta = _context.DoubleJumpTimeout;
            }
            else if (_context.DoubleJumpTimeoutDelta <= 0f && _context.CanDoubleJump)
            {
                Vector3 value = _context.Rigidbody.velocity;
                value.y = _context.JumpPower;
                _context.Rigidbody.velocity = value;
                _context.CanDoubleJump = false;
            }
        }

        
    }
}
