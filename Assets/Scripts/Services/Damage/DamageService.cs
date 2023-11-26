using Additional.Game;
using GamePlay.Damage;

namespace Services.Damage
{
    public class DamageService : MonoSingleton<DamageService>
    {
        private StatusHandler[] _handlers;


        protected override void Awake()
        {
            base.Awake();
            InitHandlers();
        }

        public void Process(DamageData damageData)
        {
            foreach (StatusHandler handler in _handlers) 
                handler.Handle(damageData);
        }

        private void InitHandlers()
        {
            _handlers = new StatusHandler[]
            {
                new MainDamageHandler(),
                new PoisonHandler(),
                new FlameHandler(),
                new DisposeHandler(),
            };
        }
    }
}