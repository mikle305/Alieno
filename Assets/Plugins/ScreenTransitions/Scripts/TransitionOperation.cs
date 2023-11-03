using System;

namespace ScreenTransitions
{
    public class TransitionOperation
    {
        public event Action OnCompleted;
        
        public void Complete()
        {
            OnCompleted?.Invoke();
        }
    }
}