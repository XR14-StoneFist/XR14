using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {}
    
    public override void EnterState()
    {
        Vector3 value = _context.PlayerComponent.RigidbodyComponent.velocity;
        value.y = _context.PlayerComponent.JumpPower;
        _context.PlayerComponent.RigidbodyComponent.velocity = value;
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
        if (_context.PlayerComponent.RigidbodyComponent.velocity.y < 0)
            SwitchState(_factory.Fall());
        if (_context.PlayerComponent.IsGrounded)
            SwitchState(_factory.Idle());
    }
}
