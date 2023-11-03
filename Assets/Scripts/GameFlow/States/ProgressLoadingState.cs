using GameFlow.Context;
using Services.Save;

namespace GameFlow.States
{
    public class ProgressLoadingState : State
    {
        private readonly GameStateMachine _context;
        private readonly SaveService _saveService;


        public ProgressLoadingState(GameStateMachine context)
        {
            _context = context;
            _saveService = SaveService.Instance;
        }

        public override void Enter()
        {
            _saveService.Load();
            _context.Enter<MainMenuState>();
        }
    }
}