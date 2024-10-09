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

	public PlayerBaseState Grounded()
	{
		return new PlayerGroundedState(_context, this);
	}

	public PlayerBaseState Air()
	{
		return new PlayerAirState(_context, this);
	}
	
	public PlayerBaseState Hang()
	{
		return new PlayerHangState(_context, this);
	}
	
	public PlayerBaseState Hold()
	{
		return new PlayerHoldState(_context, this);
	}
}
