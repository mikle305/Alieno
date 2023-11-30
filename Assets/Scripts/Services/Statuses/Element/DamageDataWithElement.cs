using GamePlay.Statuses;

namespace Services.Statuses
{
    public class DamageDataWithElement<TStatus> 
        where TStatus : ElementStatus
    {
        public DamageData DamageData { get; set; }
        public TStatus Status { get; set; }
    }
}