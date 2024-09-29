using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public Player PlayerComponent { get; private set; }
    
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    
    public PlayerBaseState CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    private void Awake()
    {
        PlayerComponent = GetComponent<Player>();
        
        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState.UpdateState();
    }
}
