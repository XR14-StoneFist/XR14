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
		    SwitchState(_factory.Fall());
	    if (_context.IsGrounded)
		    SwitchState(_factory.Idle());
    }
}
