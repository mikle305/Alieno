namespace Services
{
    public abstract class StatusHandler
    {
        public abstract bool Handle(DamageData damageData);
    }

    public abstract class StatusHandler<TStatus> : StatusHandler
        where TStatus : Status
    {
        public sealed override bool Handle(DamageData damageData)
        {
            if (!damageData.Statuses.TryGetValue(typeof(TStatus), out Status status))
                return true;
                
            return OnHandle(damageData, status as TStatus);
        }

        protected abstract bool OnHandle(DamageData damageData, TStatus status);
    }
}