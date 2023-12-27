using Services.Save;

namespace GameFlow.States
{
    public class BootState : State
    {
        private readonly GameStateMachine _context;
        private readonly SaveService _saveService;


        public BootState(GameStateMachine context, SaveService saveService)
        {
            _context = context;
            _saveService = saveService;
        }

        public override void Enter()
        {
            _saveService.Load();
            _context.Enter<MainMenuState>();
        }
    }
}