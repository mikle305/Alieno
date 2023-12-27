using Services.Factories;
using VContainer.Unity;

namespace GameFlow
{
    public class GameStateMachine : ITickable
    {
        private readonly ObjectActivator _objectActivator;
        private State _currentState;


        public GameStateMachine(ObjectActivator objectActivator)
        {
            _objectActivator = objectActivator;
        }
        
        public void Enter<T>() 
            where T : State
        {
            _currentState?.Exit();
            _currentState = GetState<T>();
            _currentState.Enter();
        }

        public void Enter<T, TNext>() 
            where T : State 
            where TNext : State
        {
            _currentState?.Exit();
            _currentState = GetState<T>();
            _currentState.Enter<TNext>();
        }

        public void Tick() 
            => _currentState.Tick();

        private T GetState<T>() where T : State
            => _objectActivator.Create<T>();
    }
}