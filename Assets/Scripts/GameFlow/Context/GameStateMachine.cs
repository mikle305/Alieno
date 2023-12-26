using GameFlow.States;
using VContainer.Unity;

namespace GameFlow.Context
{
    public class GameStateMachine : ITickable
    {
        private State _currentState;
        private readonly GameStateFactory _stateFactory;


        public GameStateMachine(GameStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }
        
        public void Enter<T>() where T : State
        {
            _currentState?.Exit();
            _currentState = GetState<T>();
            _currentState.Enter();
        }

        public void Enter<T, TNext>() where T : State where TNext : State
        {
            _currentState?.Exit();
            _currentState = GetState<T>();
            _currentState.Enter<TNext>();
        }

        public void Tick() 
            => _currentState.Tick();

        private T GetState<T>() where T : State
            => _stateFactory.Create<T>();
    }
}