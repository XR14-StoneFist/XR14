using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerBaseState
{
    public PlayerDoubleJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {}

    public override void EnterState()
    {
	    Vector3 value = _context.Rigidbody.velocity;
	    value.y = _context.JumpPower;
	    _context.Rigidbody.velocity = value;
	    _context.CanDoubleJump = false;
	    _context.Animator.SetTrigger("Jump");
	    _context.DoubleJumpEffect.Play();
    }

    public override void UpdateState()
    {
	    CheckSwitchStates();
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchStates()
    {
	    if (_context.Rigidbody.velocity.y < 0)
	    {
		    SwitchState(_factory.Fall());
	    }
	    else if (_context.IsGrounded)
	    {
		    SwitchState(_factory.Idle());
	    }
	    else if (_context.hangWallState != HangWallState.None)
	    {
		    SwitchState(_factory.Hang());
	    }
    }
}
