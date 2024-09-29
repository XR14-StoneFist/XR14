using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
	private PlayerStateMachine _context;

	public PlayerStateFactory(PlayerStateMachine currentContext)
	{
		_context = currentContext;
	}

	public PlayerBaseState Idle()
	{
		return new PlayerIdleState(_context, this);
	}
	
	public PlayerBaseState Run()
	{
		return new PlayerRunState(_context, this);
	}
	
	public PlayerBaseState Jump()
	{
		return new PlayerJumpState(_context, this);
	}
	
	public PlayerBaseState DoubleJump()
	{
		return new PlayerDoubleJumpState(_context, this);
	}
	
	public PlayerBaseState Fall()
	{
		return new PlayerFallState(_context, this);
	}
	
	public PlayerBaseState Hang()
	{
		return new PlayerHangState(_context, this);
	}
	
	public PlayerBaseState Hold()
	{
		return new PlayerHoldState(_context, this);
	}
	
	public PlayerBaseState Dash()
	{
		return new PlayerDashState(_context, this);
	}
}
