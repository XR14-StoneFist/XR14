using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
	protected PlayerStateMachine _context;
	protected PlayerStateFactory _factory;

	public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
	{
		_context = currentContext;
		_factory = playerStateFactory;
	}
	
	public abstract void EnterState();
	
	public abstract void UpdateState();
	
	public abstract void ExitState();

	public abstract void CheckSwitchStates();
	
	protected void SwitchState(PlayerBaseState newState)
	{
		ExitState();
		newState.EnterState();
		_context.CurrentState = newState;
	}
}
