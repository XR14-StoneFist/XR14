using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
	public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
		: base(currentContext, playerStateFactory) {}
	
	public override void EnterState()
	{
		_context.CanJump = true;
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
		if (!_context.IsGrounded)
		{
			SwitchState(_factory.Air());
		}
	}

	private void Jump()
	{
		if (Input.GetKeyDown(KeyBind.JumpKeyCode) && _context.CanJump)
		{
			Vector3 value = _context.Rigidbody.velocity;
			value.y = _context.JumpPower;
			_context.Rigidbody.velocity = value;
			_context.CanJump = false;
			_context.DoubleJumpTimeoutDelta = _context.DoubleJumpTimeout;
		}
	}
}
