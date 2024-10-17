using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    public PlayerDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {}

    public override void EnterState()
    {
	    Vector2 direction = (_context.StartMousePosition - _context.EndMousePosition).normalized;
	    Vector3 value = _context.Rigidbody.velocity;
	    value.x = direction.x * _context.DashPower;
	    value.y = direction.y * _context.DashPower;
	    _context.Rigidbody.velocity = value;
	    _context.CanDoubleJump = true;
	    _context.Animator.SetTrigger("Dash");
	    _context.DashEffect.Play();
    }

    public override void UpdateState()
    {
	    CheckSwitchStates();
    }

    public override void ExitState()
    {
	    _context.VelocityX = _context.Rigidbody.velocity.x;
    }

    public override void CheckSwitchStates()
    {
	    if (_context.IsGrounded)
	    {
		    SwitchState(_factory.Idle());
	    }
	    else if (_context.Rigidbody.velocity.y < 0)
	    {
		    SwitchState(_factory.Fall());
	    }
	    else if (Input.GetKeyDown(KeyBind.JumpKeyCode) && _context.CanDoubleJump)
	    {
		    SwitchState(_factory.DoubleJump());
	    }
	    else if (_context.hangWallState != HangWallState.None)
	    {
		    SwitchState(_factory.Hang());
	    }
    }
}
