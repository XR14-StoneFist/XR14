using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
	public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
		: base(currentContext, playerStateFactory) {}
	
	public override void EnterState()
	{

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
		if (!Input.GetKey(KeyBind.MoveLeftKeyCode) && !Input.GetKey(KeyBind.MoveRightKeyCode))
		{
			SwitchState(_factory.Idle());
		}
		else if (Input.GetKeyDown(KeyBind.JumpKeyCode) && _context.CanJump)
		{
			SwitchState(_factory.Jump());
		}
	}
}
