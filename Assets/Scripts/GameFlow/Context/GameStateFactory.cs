using GameFlow.States;
using VContainer;

namespace GameFlow.Context
{
    public class GameStateFactory
    {
        private readonly IObjectResolver _resolver;

        
        public GameStateFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public TState Create<TState>() where TState : State
            => _resolver.Resolve<TState>();
    }
}