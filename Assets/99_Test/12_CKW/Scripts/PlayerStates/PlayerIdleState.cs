using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
	public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
		: base(currentContext, playerStateFactory) {}
	
	public override void EnterState()
	{
		_context.CanJump = true;
		_context.CanDoubleJump = true;
		_context.Animator.SetTrigger("Idle");
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
		if (Input.GetKeyDown(KeyBind.JumpKeyCode) && _context.CanJump)
		{
			SwitchState(_factory.Jump());
		}
		else if (Input.GetKeyDown(KeyBind.MoveLeftKeyCode) || Input.GetKeyDown(KeyBind.MoveRightKeyCode))
		{
			SwitchState(_factory.Run());
		}
		else if (_context.IsGrounded == false)
		{
			SwitchState(_factory.Fall());
		}
	}
}
