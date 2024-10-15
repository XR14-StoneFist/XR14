using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
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
        if (_context.IsGrounded)
        {
            SwitchState(_factory.Idle());
        }
        else if (Input.GetKeyDown(KeyBind.JumpKeyCode) && _context.CanDoubleJump)
        {
            SwitchState(_factory.DoubleJump());
        }
        else if (_context.hangWallState != HangWallState.None)
        {
            SwitchState(_factory.Hang());
        }
        else if (_context.DashFlame && Input.GetMouseButtonDown(1))
        {
            SwitchState(_factory.Hold());
        }
    }
}
