using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerBaseState
{
	public PlayerDoubleJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
		: base(currentContext, playerStateFactory) {}

	public override void EnterState()
	{
	}

	public override void UpdateState()
	{
	}

	public override void ExitState()
	{
		CheckSwitchStates();
	}

	public override void CheckSwitchStates()
	{
	}
}