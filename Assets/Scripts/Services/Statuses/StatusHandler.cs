using GamePlay.Statuses;

namespace Services.Statuses
{
    /// <summary>
    /// Inherit when not ability
    /// </summary>
    public abstract class StatusHandler
    {
        public abstract bool Work(DamageData damageData);
    }

    /// <summary>
    /// Inherit when ability
    /// </summary>
    /// <typeparam name="TStatus"></typeparam>
    public abstract class StatusHandler<TStatus> : StatusHandler
        where TStatus : Status
    {
        public sealed override bool Work(DamageData damageData)
        {
            if (!damageData.Statuses.TryGetValue(typeof(TStatus), out Status status))
                return true;
                
            return OnHandle(damageData, status as TStatus);
        }

        protected abstract bool OnHandle(DamageData damageData, TStatus status);
    }
}