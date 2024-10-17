using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldState : PlayerBaseState
{
	public PlayerHoldState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
		: base(currentContext, playerStateFactory) {}

	public override void EnterState()
	{
		_context.Rigidbody.isKinematic = true;
		_context.StartMousePosition = new Vector2(Input.mousePosition.x , Input.mousePosition.y);
		_context.DashArrowObject = UIManager.Instance.CreateDashArrow(_context.DashFlame.transform.position);
	}

	public override void UpdateState()
	{
		CheckSwitchStates();
		UIManager.Instance.MoveDashArrow(_context.DashArrowObject, _context.DashFlame.transform.position);
		_context.EndMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Vector3 direction = _context.EndMousePosition - _context.StartMousePosition;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		_context.DashArrowObject.transform.rotation = Quaternion.Euler(0, 0, angle);
		_context.DashWrap.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
	}

	public override void ExitState()
	{
		_context.Rigidbody.isKinematic = false;
		_context.EndMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		_context.DestroyDashArrowObject();
	}

	public override void CheckSwitchStates()
	{
		if (Input.GetMouseButtonUp(1))
		{
			SwitchState(_factory.Dash());
		}
	}
}
