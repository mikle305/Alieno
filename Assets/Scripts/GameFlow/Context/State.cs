namespace GameFlow
{
    public abstract class State
    {
        public virtual void Enter() {}

        public virtual void Enter<TState>() where TState : State { }

        public virtual void Exit() {}
        
        public virtual void Tick() {}
    }
}