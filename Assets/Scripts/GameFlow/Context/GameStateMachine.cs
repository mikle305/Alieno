using System;
using System.Collections.Generic;
using Additional.Utils;
using GameFlow.States;
using UnityEngine;

namespace GameFlow.Context
{
    public class GameStateMachine
    {
        private State _currentState;
        private readonly Dictionary<Type, State> _states = new();


        public void Enter<T>() where T : State
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            Debug.Log(_currentState.GetType().Name);
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void AddState(State state)
        {
            Type type = state.GetType();
            if (_states.ContainsKey(type))
                ThrowUtils.StateDuplicated();
            else
                _states[type] = state;
        }
    }
}