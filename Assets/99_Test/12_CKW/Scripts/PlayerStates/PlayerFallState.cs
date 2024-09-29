using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
	public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
		: base(currentContext, playerStateFactory) {}

	public override void EnterState()
	{
		Debug.Log("Enter FALL");
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
		if (_context.PlayerComponent.IsGrounded)
			SwitchState(_factory.Idle());
	}
}