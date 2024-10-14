using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangState : PlayerBaseState
{
	public PlayerHangState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
		: base(currentContext, playerStateFactory) {}

	private float hangTime;
	
	public override void EnterState()
	{
		_context.CanJump = false;
		_context.CanDoubleJump = true;
		hangTime = 3f;
	}

	public override void UpdateState()
	{
		hangTime -= Time.deltaTime;
		CheckSwitchStates();
	}

	public override void ExitState()
	{
		
	}

	public override void CheckSwitchStates()
	{
		if (Input.GetKeyDown(KeyBind.JumpKeyCode) && _context.CanDoubleJump)
		{
			if (_context.hangWallState == HangWallState.Left)
			{
				_context.VelocityX = _context.WallJumpPower;
			}
			else if (_context.hangWallState == HangWallState.Right)
			{
				_context.VelocityX = -_context.WallJumpPower;
			}
			_context.hangWallState = HangWallState.None;
			SwitchState(_factory.DoubleJump());
		}
		else if (hangTime <= 0f)
		{
			_context.hangWallState = HangWallState.None;
			_context.Rigidbody.useGravity = true;
			SwitchState(_factory.Fall());
		}
	}
}