using StaticData.Abilities;

namespace GamePlay.Abilities
{
    public abstract class AbilityComponent
    {
        public abstract void Init(AbilitiesEntity entity, AbilityData data);
        public virtual void OnTick() {  }
        public virtual void OnCall() { }
    }

    public abstract class AbilityComponent<TData> : AbilityComponent
        where TData : AbilityData
    {
        protected TData Data { get; private set; }
        protected AbilitiesEntity Entity { get; private set; }

        
        public sealed override void Init(AbilitiesEntity entity, AbilityData data)
        {
            Entity = entity;
            Data = data as TData;
            OnInit();
        }
        
        protected virtual void OnInit() { }
    }
}